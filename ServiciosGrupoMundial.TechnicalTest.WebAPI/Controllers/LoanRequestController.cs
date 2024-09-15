namespace ServiciosGrupoMundial.TechnicalTest.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanRequestController(EvaluateLoanRequest evaluator) : ControllerBase
{
    private readonly EvaluateLoanRequest _evaluator = evaluator
         ?? throw new ArgumentNullException(nameof(evaluator));

    // <summary>
    /// Validates a loan request based on XML data provided in the request body.
    /// </summary>
    /// <param name="stringXML">An object containing the XML string representing the loan request.</param>
    /// <returns>An <see cref="IActionResult"/> with validation results.</returns>
    [HttpPost]
    public IActionResult Validate([FromBody] LoanRequestXML stringXML)
    {
        if (stringXML == null) return BadRequest("No se ha proporcionado un XML válido");

        try
        {
            // Create a LoanRequest object from the XML data elements
            var loanRequest = new LoanRequest(stringXML.DataElements);
            // Use the evaluator service to validate the loan request
            var response = _evaluator.Validate(loanRequest);

            if (response.Code < 0) return BadRequest(response.Message);

            return Ok(response.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Details = e.Message });
        }
    }
}