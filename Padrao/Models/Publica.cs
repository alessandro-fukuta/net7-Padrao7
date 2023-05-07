namespace Oficina7.Models
{
	public static class Publica
	{
		public static bool Logado { get; set; } // true logado, false (não-logado)
		public static string? Login_Usuario { get; set; } // usuario logado
		public static bool Login_Administrador { get; set; } // usuario administrador

		// dados para enviar email ao usuario
		public static string? Login_NomeCompleto { get; set; } // nome completo
		public static string? Login_Email { get; set; }
		public static int Login_Usuario_Id { get; set; }
		// sobre o sistema
		public static string? Sistema_Nome { get; set; }
		public static string? Sistema_Versao { get; set; }

		public static string? Mensagem1 { get; set; }
		public static string? Mensagem2 { get; set; }


	}
}
