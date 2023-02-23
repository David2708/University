namespace universityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool validateIsUserSigningKey { get; set; } // comprobar firma del usuario
        public string IssuerSigningKey { get; set; } = string.Empty;

        public bool validateIssuer { get; set; } = true;
        public string? validIssuer { get; set; }

        public bool validateAudience { get; set; } = true;
        public string? validAudience { get; set; }

        public bool RequireExpirationTime { get; set; }
        public bool validateLifeTime { get; set; } = true;

    }
}
