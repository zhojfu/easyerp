namespace Domain.Model.Security
{
    public interface IAclSupported
    {
        bool SubjectToAcl { get; set; }
    }
}