namespace DotFramework.Core.Web
{
    public class AuthorizationToken
    {
        public AuthorizationToken(TokenTypeEnum tokenType, string token)
        {
            TokenType = tokenType;
            Token = token;
        }

        public TokenTypeEnum TokenType { get; private set; } = TokenTypeEnum.Bearer;
        public string Token { get; private set; }
    }

    public enum TokenTypeEnum
    {
        Basic,
        Bearer,
    }
}
