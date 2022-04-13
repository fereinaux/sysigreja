using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Services
{
    public static class UtilServices
    {
        public static int GetAge(DateTime? dataNascimento)
        {
            if (!dataNascimento.HasValue)
            {
                return 0;
            }
            var age = DateTime.Today.Year - dataNascimento.Value.Year;
            if (dataNascimento > DateTime.Today.AddYears(-age)) age--;

            return age;
        }

        public static string DecimalToMoeda(decimal input)
        {
            return $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", input)}";
        }

        public static string CapitalizarNome(string nome)
        {
            if (nome != null)
            {

                string[] excecoes = new string[] { "e", "de", "da", "das", "do", "dos" };
                var palavras = new Queue<string>();
                foreach (var palavra in nome.Split(' '))
                {
                    if (!string.IsNullOrEmpty(palavra))
                    {
                        var emMinusculo = palavra.ToLower();
                        var letras = emMinusculo.ToCharArray();
                        if (!excecoes.Contains(emMinusculo)) letras[0] = char.ToUpper(letras[0]);
                        palavras.Enqueue(new string(letras));
                    }
                }
                return string.Join(" ", palavras);
            }
            return "";
        }
    }
}