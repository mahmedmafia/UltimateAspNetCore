namespace Entities.Exeptions
{
    public sealed class MaxAgeRangeBadRequestException : BadRequestException
    {
        public MaxAgeRangeBadRequestException() : base("max age cant be less than min age")
        {
        }
    }
}
