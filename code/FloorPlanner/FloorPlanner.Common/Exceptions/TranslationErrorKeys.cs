namespace FloorPlanner.Common.Exceptions;

public sealed class TranslationErrorKeys
{
    public const string TranslationErrorCategory = "Errors";

    public static readonly string InternalServerErrorKey = string.Concat(TranslationErrorCategory, ".", "InternalServerError");
}