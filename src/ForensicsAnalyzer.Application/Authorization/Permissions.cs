using System.Reflection;

namespace ForensicsAnalyzer.Application.Authorization;

public static class Permissions
{
    public static class Cases
    {
        public const string Create = "Cases.Create";
        public const string Read   = "Cases.Read";
        public const string Update = "Cases.Update";
        public const string Delete = "Cases.Delete";
        public const string Assign = "Cases.Assign";
    }

    public static class Users
    {
        public const string Read        = "Users.Read";
        public const string Update      = "Users.Update";
        public const string Delete      = "Users.Delete";
        public const string ManageRoles = "Users.ManageRoles";
    }

    public static class Sources
    {
        public const string Create = "Sources.Create";
        public const string Read   = "Sources.Read";
        public const string Update = "Sources.Update";
        public const string Delete = "Sources.Delete";
    }

    public static class Artifacts
    {
        public const string Create = "Artifacts.Create";
        public const string Read   = "Artifacts.Read";
        public const string Update = "Artifacts.Update";
        public const string Delete = "Artifacts.Delete";
    }

    public static class Social
    {
        public const string Create = "Social.Create";
        public const string Read   = "Social.Read";
        public const string Update = "Social.Update";
        public const string Delete = "Social.Delete";
    }

    public static class Admin
    {
        public const string ImportDatabase = "Admin.ImportDatabase";
        public const string ManageRoles    = "Admin.ManageRoles";
    }

    public static IEnumerable<string> GetAll() =>
        typeof(Permissions)
            .GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!);
}
