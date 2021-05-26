namespace Demo.DDD.Integration.Database
{
    public class DatabaseSettings
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 1433;
        public bool Pooling { get; set; }
        public bool MultiSubnetFailover { get; set; }

        public string GetConnectionString()
        {
            var security = IntegratedSecurity ? "Integrated Security=True;" : $"User Id={UserId};Password={Password};";
            return $"Data Source={DataSource},{Port};Initial Catalog={InitialCatalog};{security}Pooling={Pooling};MultiSubnetFailover={MultiSubnetFailover}";
        }
    }
}
