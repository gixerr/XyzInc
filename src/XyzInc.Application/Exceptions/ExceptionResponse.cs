using System.Net;

namespace XyzInc.Application.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);