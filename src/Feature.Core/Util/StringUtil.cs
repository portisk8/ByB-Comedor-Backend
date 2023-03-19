using System.Text;
using System.Text.RegularExpressions;

namespace Feature.Core.Util
{
    public static class StringUtil
    {
        private static readonly HashSet<char> _defaultNonWordCharacters = 
                new HashSet<char> { ',', '.', ':', ';' };

        private static readonly string _espacioEnBlanco = " ";

        private const string PatternLetraAConAcentos = "[á|à|ä|â]";
        private const string PatternLetraEConAcentos = "[é|é|è|ë|ê]";
        private const string PatternLetraIConAcentos = "[í|ì|ï|î]";
        private const string PatternLetraOConAcentos = "[ó|ò|ö|ô]";
        private const string PatternLetraUConAcentos = "[ú|ù|ü|û]";
        private const string PatternLetraEnieMinuscula = "[ñ]";
        private const string PatternLetraEnieMayuscula = "[Ñ]";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadena">Cadena a recortar</param>
        /// <param name="cantidadCaracteres">Cantidad de caracteres a recortar</param>
        /// <param name="mantenerPalabras">Si recorta o no palabras</param>
        /// <param name="sufijoDeRellenoSiSuperaLongitud"></param>
        /// <returns></returns>
        public static string Recortar(string cadena, int cantidadCaracteres,
                                bool mantenerPalabras, string sufijoDeRellenoSiSuperaLongitud = "...")
        {
            //Verificando la cadena
            if (string.IsNullOrEmpty(cadena)) return string.Empty;

            //Verificando cantidad a recortar
            if (cantidadCaracteres <= 0) return string.Empty;
            
            if (mantenerPalabras)
            {
                var posicionABuscarEspacioEnBlanco = cantidadCaracteres - 1;

                if (cadena.Length < cantidadCaracteres || cadena.IndexOf(_espacioEnBlanco, posicionABuscarEspacioEnBlanco, StringComparison.Ordinal) == -1)
                    return cadena;

                return string.Format("{0}{1}", 
                                    cadena.Substring(0, cadena.IndexOf(_espacioEnBlanco, posicionABuscarEspacioEnBlanco, StringComparison.Ordinal)), 
                                    sufijoDeRellenoSiSuperaLongitud);
                
            }

            //Verificar la cantidad de caracteres es mayor entonces recortar
            if (cadena.Length > cantidadCaracteres)
            {
                return string.Format("{0}{1}", cadena.Substring(0, cantidadCaracteres), sufijoDeRellenoSiSuperaLongitud);

            }

            return cadena;

        }



        ///  FUNCTION Enquote Public Domain 2002 JSON.org 
        ///  @author JSON.org 
        ///  @version 0.1 
        ///  Ported to C# by Are Bjolseth, teleplan.no 
        public static string Enquote(string s)
        {
            if (s == null || s.Length == 0)
            {
                return "\"\"";
            }
            char c;
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;

            sb.Append('"');
            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                if ((c == '\\') || (c == '"') || (c == '>'))
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\r')
                    sb.Append("\\r");
                else
                {
                    if (c < ' ')
                    {
                        //t = "000" + Integer.toHexString(c); 
                        string tmp = new string(c, 1);
                        t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            sb.Append('"');
            return sb.ToString();
        }



        /// <summary>
        /// Limpia el texto para la URL
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string LimpiarTextoParaURL(string texto)
        {

            string textoParaURL;
            textoParaURL = texto.Trim().Replace("-", string.Empty);
            textoParaURL = LimpiarEspaciosEnBlancoDobles(textoParaURL.Trim());
            textoParaURL = textoParaURL.Trim().Replace(" ", "-");
            textoParaURL = RemoverAcentos(textoParaURL);
            textoParaURL = Regex.Replace(textoParaURL, "[^0-9a-zA-Z_-]", "");

            return textoParaURL;

        }

        public static string LimpiarEspaciosEnBlanco(string texto)
        {
            return Regex.Replace(texto, @"\s", string.Empty);
        }

        public static string LimpiarEspaciosEnBlancoDobles(string texto)
        {
            return Regex.Replace(texto, @" {2,}", _espacioEnBlanco);
        }

        public static string LimpiarTextoParaKeywords(string texto)
        {
            string palabrasClaves = RemoverAcentos(texto);
            palabrasClaves = RemoverPreposiciones(palabrasClaves);

            string[] palabrasClavesArray = palabrasClaves.Split(' ');
            return string.Join(", ", palabrasClavesArray);

        }


        /// <summary>
        /// Remover acentos por su correspondiente caracter absoluto
        /// Ejemplo á -> a
        /// </summary>
        /// <param name="cadena">Cadena para manipular</param>
        /// <returns></returns>
        public static string RemoverAcentos(string cadena)
        {
            //Version 1
            //----------------------------
            //string normalizedString = cadena.Normalize(NormalizationForm.FormD);
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i <= normalizedString.Length - 1; i++)
            //{
            //    Globalization.UnicodeCategory uc = Globalization.CharUnicodeInfo.GetUnicodeCategory(normalizedString(i));
            //    if (uc != Globalization.UnicodeCategory.NonSpacingMark)
            //    {
            //        sb.Append(normalizedString(i));
            //    }
            //}
            //return (sb.ToString().Normalize(NormalizationForm.FormC));

            //Version 2
            //----------------------------
            //byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(cadena);
            //return System.Text.Encoding.UTF8.GetString(bytes);


            //Version 3
            //----------------------------
            Regex replace_a_Accents = new Regex(PatternLetraAConAcentos, RegexOptions.Singleline);
            Regex replace_a_AccentsUpper = new Regex(PatternLetraAConAcentos.ToUpper(), RegexOptions.Singleline);
            Regex replace_e_Accents = new Regex(PatternLetraEConAcentos, RegexOptions.Singleline);
            Regex replace_e_AccentsUpper = new Regex(PatternLetraEConAcentos.ToUpper(), RegexOptions.Singleline);
            Regex replace_i_Accents = new Regex(PatternLetraIConAcentos, RegexOptions.Singleline);
            Regex replace_i_AccentsUpper = new Regex(PatternLetraIConAcentos.ToUpper(), RegexOptions.Singleline);
            Regex replace_o_Accents = new Regex(PatternLetraOConAcentos, RegexOptions.Singleline);
            Regex replace_o_AccentsUpper = new Regex(PatternLetraOConAcentos.ToUpper(), RegexOptions.Singleline);
            Regex replace_u_Accents = new Regex(PatternLetraUConAcentos, RegexOptions.Singleline);
            Regex replace_u_AccentsUpper = new Regex(PatternLetraUConAcentos.ToUpper(), RegexOptions.Singleline);
            Regex replace_enie_Accents = new Regex(PatternLetraEnieMinuscula, RegexOptions.Singleline);
            Regex replace_enie_AccentsUpper = new Regex(PatternLetraEnieMayuscula, RegexOptions.Singleline);

            cadena = replace_a_Accents.Replace(cadena, "a");
            cadena = replace_a_AccentsUpper.Replace(cadena, "A");
            cadena = replace_e_Accents.Replace(cadena, "e");
            cadena = replace_e_AccentsUpper.Replace(cadena, "E");
            cadena = replace_i_Accents.Replace(cadena, "i");
            cadena = replace_i_AccentsUpper.Replace(cadena, "I");
            cadena = replace_o_Accents.Replace(cadena, "o");
            cadena = replace_o_AccentsUpper.Replace(cadena, "O");
            cadena = replace_u_Accents.Replace(cadena, "u");
            cadena = replace_u_AccentsUpper.Replace(cadena, "U");

            cadena = replace_enie_Accents.Replace(cadena, "n");
            cadena = replace_enie_AccentsUpper.Replace(cadena, "N");

            return cadena;

            //HttpUtility.UrlEncode(nombre.Replace(" ", "_"));



        }

        public static string RemoverPreposiciones(string cadena)
        {



            //a, ante, bajo, cabe, con, contra, de, desde, en, entre, hacia, hasta, para, por, según, sin, so, sobre, tras
            string[] preposiciones1 = new string[]
                                          {
                                              //Preposiciones (lista tradicional)
                                              "a", "ante", "bajo", "cabe", "con", "contra", "de", "desde", "en", "entre",
                                              "hacia", "hasta", "para", "por", "según", "segun", "sin", "so", "sobre", "tras",
                                              //Preposiciones (lista RAE)
                                              "durante", "mediante", "pro", "vía", "via",


                                              //Articulos
                                              "el", "la", "los", "las", "al", "del", "un", "una", "unos", "unas"
                                          };

            string[] cadenaArray = cadena.Split(' ');
            List<string> cadenaLimpia = new List<string>();

            foreach (var palabra in cadenaArray)
            {
                if (string.IsNullOrEmpty(palabra.Trim()))
                {
                    continue;

                }

                var indice = Array.IndexOf(preposiciones1, palabra.ToLower());
                if (indice < 0)
                {
                    cadenaLimpia.Add(palabra);
                }
            }

            return string.Join(
                          ",",
                          Array.ConvertAll(
                             cadenaLimpia.ToArray(),
                             element => element.ToString()
                          )
                        );
        }

        /// <summary>
        /// Formatear cadena tipo oracion
        /// </summary>
        /// <param name="cadena">Cadena a formatear</param>
        /// <returns></returns>
        public static string PrimeraLetraMayuscula(string cadena)
        {
            string primera;
            primera = cadena[0].ToString().ToUpper();

            if (cadena.Length > 1)
            {
                return string.Concat(primera, cadena.Substring(1));
            }

            return primera;
        }


        /// <summary>
        /// Remieve saltos de linea
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static string RemoverSaltosDeLinea(string cadena)
        {
            //Limpiar saltos de linea en bajada para JSON
            Byte[] myBytes13 = { 13 };
            string myStr13 = Encoding.ASCII.GetString(myBytes13);
            Byte[] myBytes10 = { 10 };
            string myStr10 = Encoding.ASCII.GetString(myBytes10);

            var cadenaLimpia = cadena;
            cadenaLimpia = Regex.Replace(cadenaLimpia, myStr13, string.Empty, RegexOptions.Multiline);
            cadenaLimpia = Regex.Replace(cadenaLimpia, myStr10, string.Empty, RegexOptions.Multiline);

            return cadenaLimpia;
        }
    }
}
