using CleanArchitecture.Application.Rentals.ReservRent;
using CleanArchitecture.Application.Rentals.ReservRent.GetRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CleanArchitecture.Api.Controllers.Rentals;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly ISender _sender;

    public RentalsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRent(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetRentalQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost()]
    public async Task<IActionResult> ReservRent(
       RentReservRequest request,
       CancellationToken cancellationToken
   )
    {
        var command = new ReservRentCommand(
            request.VehiculoId,
            request.UserId,
            request.StartDate,
            request.EndDate
        );

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
        ? CreatedAtAction(nameof(GetRent), new { id = result.Value }, result.Value)
        : BadRequest();
    }

}
