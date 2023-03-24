using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandAPIRepo _repository;
    private readonly IMapper _mapper;
    public CommandsController(ICommandAPIRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
        var commandItems = _repository.GetAllCommands();

        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    [HttpGet("{id}", Name="GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        var commandItem = _repository.GetCommandById(id);

        if (commandItem == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
        var newCommand = _mapper.Map<Command>(commandCreateDto);
        _repository.CreateCommand(newCommand);
        _repository.SaveChanges();
        
        var commandReadDto = _mapper.Map<CommandReadDto>(newCommand);
        return CreatedAtRoute(nameof(GetCommandById),
            new { Id = commandReadDto.Id },
            commandReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
        var commandFromRepo = _repository.GetCommandById(id);
        // 404 if not found
        if (commandFromRepo is null)
        {
            return NotFound();
        }
        // update command - map dto to command
        _mapper.Map(commandUpdateDto,commandFromRepo);
        // update nothing
        _repository.UpdateCommand(commandFromRepo);
        _repository.SaveChanges();
        // 204 code
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
        var commandFromRepo = _repository.GetCommandById(id);
        if (commandFromRepo is null)
        {
            return NotFound();
        }

        // create placeholder based on command
        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandFromRepo);
        // apply the patch document
        patchDoc.ApplyTo(commandToPatch, ModelState);
        // validate model changes, i.e. model validation Data Annotations
        if (!TryValidateModel(commandToPatch))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(commandToPatch, commandFromRepo);
        _repository.UpdateCommand(commandFromRepo);
        _repository.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        var commandFromRepo = _repository.GetCommandById(id);
        if (commandFromRepo is null)
        {
            return NotFound();
        }

        _repository.DeleteCommand(commandFromRepo);
        _repository.SaveChanges();
        return NoContent();
    }
}