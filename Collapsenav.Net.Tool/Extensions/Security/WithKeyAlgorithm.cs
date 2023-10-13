namespace Collapsenav.Net.Tool;
public abstract class WithKeyAlgorithm<Alg> : DefaultAlgorithm<Alg> where Alg : class
{
    public const string DefaultKey = "Collapsenav.Net.Tool";
}