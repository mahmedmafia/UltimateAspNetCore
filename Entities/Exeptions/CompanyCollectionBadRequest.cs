namespace Entities.Exeptions
{
    public sealed class CompanyCollectionBadRequest : BadRequestException
    {
        public CompanyCollectionBadRequest() : base("collection sent is null")
        {
        }
    }
}
