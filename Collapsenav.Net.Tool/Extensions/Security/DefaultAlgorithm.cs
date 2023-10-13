namespace Collapsenav.Net.Tool;
public abstract class DefaultAlgorithm<Alg> where Alg : class
{
    protected static Alg? Algorithm;
    public static void Clear() => Algorithm = null;
}