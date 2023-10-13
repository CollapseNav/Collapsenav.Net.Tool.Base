namespace Collapsenav.Net.Tool;
public abstract class WithKeyAndIVAlgorithm<Alg> : WithKeyAlgorithm<Alg> where Alg : class
{
    public const string DefaultIV = "looT.teN.vanespalloC";
}