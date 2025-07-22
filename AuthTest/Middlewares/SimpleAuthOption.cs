namespace AuthTest.Middlewares;

public class SimpleAuthOption
{
    public string[] ValidTokens { get; set; }
    public TokenPosition TokenPosition { get; set; }
}