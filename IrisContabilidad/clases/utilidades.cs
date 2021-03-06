﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Ionic.Zip;
using MySql.Data.MySqlClient;
using CompressionLevel = Ionic.Zlib.CompressionLevel;

namespace IrisContabilidad.clases
{
    public class utilidades
    {

        //objetos
        private empleado empleado;


        //variables fijas
        private string smtpGmail = "smtp.gmail.com";
        private int puertoGmail = 587;

        private string smtpHotmail = "smtp.live.com";
        private int puertoHotmail = 587;

        private string smtpSevenSoft = "smtpout.asia.secureserver.net";
        private int puertoSevenSoft = 3535;

        private string smtpYahoo = "smtp.mail.yahoo.com";
        private int PuertoYahoo = 465;

        public string nombreArchivo = "";



        //variables
        private string archivoDestino = "";
        private string nombreProducto = "";
        private string numeroCodigo = "";


        //variables
        string codigoFactura = "";
        double numero_de_hojas = 0;
        //PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
        PrintDialog printDialog = new PrintDialog();
        private string CodigoCobro;


        public bool primo(Int64 n)
        {


            Int64 cont = 0;
            for (Int64 f = 1; f <= n; f++)
            {
                if (n % f == 0)
                {
                    cont++;
                }
            }
            if (cont == 2)
                return true;
            else
                return false;
        }
        
        public string conv999(Int64 n)
        {
            if (n == 0) return "";
            string[] unidad = new string[] { null, "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve", "veinte", "veintiuno", "veintidos", "veintitres", "veinticuatro", "veinticinco", "veintiseis", "veintisiete", "veintiocho", "veintinueve", "trenta" };
            string[] decena = new string[] { null, "diez", "veinte", "treinta", "cuarenta", "cincuenta", 
                "sesenta", "setenta", "ochenta", "noventa" };
            string cn = n.ToString().PadLeft(3, '0');
            int c = Convert.ToInt16(cn.Substring(0, 1));
            int d = Convert.ToInt16(cn.Substring(1, 1));
            int u = Convert.ToInt16(cn.Substring(2, 1));
            int u2 = Convert.ToInt16(cn.Substring(1, 2));
            if (n == 100) return " cien ";
            string letras = "";
            if (c > 0) letras += unidad[c] + " cientos ";
            if (u2 > 0)
            {
                if (u2 < 30) letras += unidad[u2];
                else
                {
                    letras += decena[d];
                    if (u > 0) letras += " y " + unidad[u];
                }
            }
            //arreglos
            letras = letras.ToLower();
            letras = letras.Replace("uno cientos", "cientos");
            letras = letras.Replace("cinco cientos", "quinientos");
            letras = letras.Replace("siete cientos", "setecientos");
            letras = letras.Replace("nueve cientos", "novecientos");
            return letras;
        }

        public string conv15digitos(Int64 n)
        {
            string cn = n.ToString().PadLeft(18, '0');
            int n1 = Convert.ToInt16(cn.Substring(0, 3));
            int n2 = Convert.ToInt16(cn.Substring(3, 3));
            int n3 = Convert.ToInt16(cn.Substring(6, 3));
            int n4 = Convert.ToInt16(cn.Substring(9, 3));
            int n5 = Convert.ToInt16(cn.Substring(12, 3));
            int n6 = Convert.ToInt16(cn.Substring(15, 3));
            string Letras = "";
            if (n1 > 0)
                Letras += conv999(n1) + " trillones ";
            if (n2 > 0)
                Letras += conv999(n2) + " billones ";
            if (n3 > 0)
                Letras += conv999(n3) + " mil millones ";
            if (n4 > 0)
                Letras += conv999(n4) + " millones ";
            if (n5 > 0)
                Letras += conv999(n5) + " mil ";
            if (n6 > 0)
                Letras += conv999(n6) + " ";
            //arreglitos   
            Letras = Letras.ToLower();
            Letras = Letras.Replace("uno trillones", "un trillon ");
            Letras = Letras.Replace("uno billones", "un billon ");
            Letras = Letras.Replace("uno mil", "mil");
            Letras = Letras.Replace("uno  millones", "un millon  ");
            if (Letras == "millones")
                Letras = Letras.Replace(" millones", "un millon");
            //+arreglitos
            return Letras;
        }

        public void hablar(string letras)
        {
            string[] v = letras.Split();
            for (int f = 0; f < v.Length; f++)
            {
                if (string.IsNullOrEmpty(v[f]) == false)
                {
                    SoundPlayer sonar = new SoundPlayer();
                    sonar.SoundLocation = @"C:\Users\wilmer\Desktop\numeros\" + v[f] + ".wav";
                    sonar.PlaySync();
                }
            }
        }

        public void escribir(string ruta, string contenido)
        {
            StreamWriter escribir = new StreamWriter(ruta);
            escribir.WriteLine(contenido);
            escribir.Close();
        }

        public DataSet ejecutarcomando_sql(string query)
        {
            try
            {
                if (query == "")
                {
                    return null;
                }
                SqlConnection conn = new SqlConnection("Data Source=.;" + "Initial Catalog=iris;" + "User id=dextroyex;" + "Password=wilmerlomas1;");
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Fallo conectando al server.:" + ex.ToString());
                return null;
            }
        }

        public DataSet ejecutarcomando_mysql(string query)
        {
            try
            {
                if (query == "")
                {
                    MessageBox.Show("Query ingreso nulo","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return null;
                }
                MySqlConnection conn = new MySqlConnection("server=localhost;uid=bc;" + "pwd=BlackCode123;database=iris;Allow Zero Datetime=false;");
                //MySqlConnection conn = new MySqlConnection("server=153.92.11.223;uid=bc1;" + "pwd=wilmerlomas1;database=iris;");
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo conectando al server.:"+ex.ToString());
                return null;
            }
        }

        public DataSet ejecutarcomando_mysql_remoto(string query)
        {
            try
            {
                if (query == "")
                {
                    return null;
                }
                MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=dextroyex;pwd=wilmerlomas1;database=iris;");
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo conectando al server.:" + ex.ToString());
                return null;
            }
        }

        public string encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        
        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string desencriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = Encoding.Unicode.GetString(decryted);
            return result;
        }

        public bool numero_entero(string cadena)
        {
            try
            {
                Convert.ToInt64(cadena.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool numero_decimal(string cadena)
        {
            try
            {
                Convert.ToDecimal(cadena.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string numero_miles(double numero)
        {
            try
            {
                numero.ToString("###,###,N");
                return numero.ToString();
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public bool solo_letras(string cadena)
        {
            try
            {
                double x = 0;
                x = Convert.ToDouble(cadena);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public string CadenaEliminarPalabra(string cadena, string palabra)
        {
            char[] partes = cadena.ToCharArray();
            string nuevaCadena = "";
            bool pegar = true;
            int cont = 0;
            for (int i = 0; i < cadena.Length; i++)
            {
                if (partes[i] == '|')
                {
                    pegar = false;
                }
                if (partes[i] == '|')
                {
                    pegar = true;
                    i++;
                    cont++;
                }

                if (pegar)
                {
                    nuevaCadena += partes[i].ToString();
                }
                nuevaCadena.Remove(0, cont);
            }

            return nuevaCadena;
        }
        
        public Boolean comprimirArchivo(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("Archivo no existe", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                DirectoryInfo directorio = new DirectoryInfo(rutaArchivo);
                foreach (FileInfo di in directorio.GetFiles())
                {
                    Compress(di.ToString());
                }

                //comprimir
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        public Boolean Compress(string rutaArchivo)
        {
            try
            {
                // Get the stream of the source file.
                FileInfo fi = new FileInfo(rutaArchivo);
                using (FileStream inFile = fi.OpenRead())
                {
                    // Prevent compressing hidden and
                    // already compressed files.
                    if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
                    {
                        // Create the compressed file.
                        using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                        {
                            using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                            {
                                // Copy the source file into
                                // the compression stream.
                                inFile.CopyTo(Compress);

                                //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        public Boolean Decompress(string rutaArchivo)
        {
            try
            {
                FileInfo fi = new FileInfo(rutaArchivo);
                // Get the stream of the source file.
                using (FileStream inFile = fi.OpenRead())
                {
                    // Get original file extension, for example
                    // "doc" from report.doc.gz.
                    string curFile = fi.FullName;
                    string origName = curFile.Remove(curFile.Length - fi.Extension.Length);
                    //Create the decompressed file.
                    using (FileStream outFile = File.Create(origName))
                    {
                        using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                        {
                            // Copy the decompression stream
                            // into the output file.
                            Decompress.CopyTo(outFile);
                            //Console.WriteLine("Decompressed: {0}", fi.Name);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
       
        //public  Boolean limpiarDatosTodasTablasMysql()
        //{
        //    try
        //    {
        //        string sql = "select CONCAT(' SET FOREIGN_KEY_CHECKS=0; SET SQL_SAFE_UPDATES = 0; truncate table ',table_name) from information_schema.tables where table_schema='punto_venta';";
        //        DataSet ds = utilidades.ejecutarcomando_mysql(sql);
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            //MessageBox.Show(row[0].ToString());
        //            utilidades.ejecutarcomando_mysql(row[0].ToString());
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.ToString());
        //        return false;
        //    }
        //}

        public string getFormaFechaNormal(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy");
        }

        public Boolean ValidarCorreo(string correo)
        {
            try
            {
                try
                {
                    Regex.IsMatch(correo,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    return true;
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validando el correo: " + correo + " -->" + ex.ToString());
                return false;
            }
        }

        public Boolean EnviarCorreo(string emisor, string password, string destinatario, string asunto, string mensaje,string ruta_archivo, int opcionCorreo)
        {
            try
            {
                MailMessage correo = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correo.Body = "";
                correo.Subject = "";
                if (mensaje.ToString() != "")
                {
                    correo.Body = mensaje;
                }
                correo.Subject = asunto;
                correo.IsBodyHtml = true;
                char[] delimitador = { ',' };
                string[] destinatariosArray = destinatario.Split(delimitador);
                //para agregar cada destinatario al mail
                foreach (string desti in destinatariosArray)
                {
                    try
                    {
                        if (ValidarCorreo(desti.ToString().Trim()))
                        {
                            correo.To.Add(desti.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error, correo no valido: " + desti.ToString());
                        return false;
                    }
                }
                if (ruta_archivo.ToString() != "")
                {
                    //Microsoft.Office.Interop.Word.System.Net.Mail.Attachment archivo = new Microsoft.Office.Interop.Word.System.Net.Mail.Attachment(ruta_archivo.ToString());
                    //correo.Attachments.Add(archivo);
                }
                correo.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor, password);
                if (opcionCorreo == 1)
                {
                    //es gmail
                    envios.Host = smtpGmail;
                    envios.Port = puertoGmail;
                    envios.EnableSsl = true;
                }
                if (opcionCorreo == 2)
                {
                    //es hotmail
                    envios.Host = smtpHotmail;
                    envios.Port = puertoHotmail;
                    envios.EnableSsl = true;
                }

                if (opcionCorreo == 4)
                {
                    //es yahoo
                    //smtp.mail.yahoo.com	587	Yes
                    envios.Host = smtpYahoo;
                    envios.Port = PuertoYahoo;
                    envios.EnableSsl = true;
                }
                correo.Priority = MailPriority.High;

                envios.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("El mensaje no en envio correctamente: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //public Boolean ImprimirCodigoBarra(string NombreProducto, string numero)
        //{
        //    try
        //    {

        //        string directorio = @"C:\Users\7SOFTCASA\Documents\GitHub\7ERP-1.0\7ADMFIC-1.0\7ADMFIC-1.0\bin\Release";
        //        directorio = Path.GetDirectoryName(directorio);//bin
        //        directorio = Path.GetDirectoryName(directorio);//7adfic-1.0
        //        directorio += @"\Temporales\";
        //        MessageBox.Show(directorio);
        //        this.nombreProducto = NombreProducto;

        //        this.numeroCodigo = numero;

        //        //generando el codigo de barra
        //        Barcode barra = new Barcode();

        //        barra.IncludeLabel = true;

        //        barra.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;

        //        barra.Encode(BarcodeLib.TYPE.CODE128, numero.ToString(), Color.Black, Color.White, 300, 100);

        //        string rutaDestino = @"C:\Users\7SOFTCASA\Desktop\Nueva carpeta-\";

        //        archivoDestino = rutaDestino + NombreProducto;

        //        barra.EncodedType = BarcodeLib.TYPE.CODE128B;

        //        barra.SaveImage(archivoDestino + ".jpg", BarcodeLib.SaveTypes.JPG);


        //        //imprimiendo la imagen generada
        //        PrintDocument pd = new PrintDocument();
        //        PaperSize paperSize = new PaperSize("Ticket Codido Barra", 110, 80);
        //        pd.DefaultPageSettings.PaperSize = paperSize;

        //        pd.PrintPage += new PrintPageEventHandler(this.ImprimirCodigoBarra_PrintEvent);
        //        PrintPreviewDialog pv = new PrintPreviewDialog();
        //        pv.Document = pd;
        //        //pd.Print();
        //        pv.ShowDialog();

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show("Error imprimiendo codigo barra ImprimirCodigoBarra.: " + ex.ToString());
        //        return false;
        //    }
        //}
        public void CopiarArchivosRecursivo(DirectoryInfo Origen, DirectoryInfo Destino)
        {
            foreach (DirectoryInfo dir in Origen.GetDirectories())
            {
                CopiarArchivosRecursivo(dir, Destino.CreateSubdirectory(dir.Name));
            }
            foreach (FileInfo file in Origen.GetFiles())
            {
                file.CopyTo(Path.Combine(Destino.FullName, file.Name));
            }
        }

        public string GetSHA1(string str)
        {
            try
            {
                SHA1 sha1 = SHA1Managed.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha1.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error.: " + ex.ToString());
                return "";
            }
        }

        public string GetMd5(string texto)
        {
            try
            {
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
                //Se utilizan las clases de encriptación MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes("1")); //Aqui se toma la llave que debe ser igual para encriptar y descencriptar
                hashmd5.Clear();
                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();
                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception)
            {

            }
            return texto;
        }
        
        public string GetBase64Encriptar(string cadena)
        {
            byte[] byt = Encoding.UTF8.GetBytes(cadena);
            return Convert.ToBase64String(byt);
        }

        public string GetBase64Desencriptar(string cadena)
        {
            byte[] b = Convert.FromBase64String(cadena);
            b = Convert.FromBase64String(cadena);
            return Encoding.UTF8.GetString(b);
        }

        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public string GetSHA512(string str)
        {
            SHA512 sha512 = SHA512Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha512.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public Boolean comprimirArchivos(List<string> rutaArchivos, string rutaDestino, string password)
        {
            try
            {
                ZipFile zip = new ZipFile();

                zip.CompressionLevel = (CompressionLevel) System.IO.Compression.CompressionLevel.Optimal;

                zip.Comment = "Compresion de jemplo";

                if (password != "")
                {
                    zip.Password = password;
                }

                rutaArchivos.ForEach(x =>
                {
                    zip.AddFile(x.ToString(), "");
                    //MessageBox.Show(x.ToString(),rutaDestino);
                });

                zip.Save(rutaDestino + "Archivo" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Millisecond + ".zip");
                zip.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error comprimirArchivos.: " + ex.ToString());
                return false;
            }
        }

        public Boolean descomprimirArchivo(string rutaArchivo, string rutaDestino, string password = null)
        {
            try
            {
                //para leer cada archivo
                ZipFile zip = ZipFile.Read(rutaArchivo);

                //si el archivo tiene password
                if (password != null)
                {
                    zip.Password = password;
                }

                //si el archivo tiene ruta especifica para descomprimir
                if (rutaDestino != "")
                {
                    zip.ExtractAll(rutaDestino);
                }

                zip.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error descomprimirArchivo.: " + ex.ToString());
                return false;
            }
        }

        public string GetTituloVentana(empleado empleadoA, string tituloVentana)
        {
            try
            {
                this.empleado = empleadoA;
                string titulo = "IRIS-";
                if (empleado != null)
                {
                    titulo += tituloVentana.ToUpper() + "-" + empleado.nombre.ToUpper();
                }
                return titulo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error GetTituloVentana.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public String getNombreMaquina()
        {
            String nombre = "";
            nombre = Environment.MachineName;

            return nombre;


        }

        public Boolean isNumeric(String cadena)
        {
            if (cadena == null)
            {

                return false;
            }
            Boolean respuesta = false;
            Decimal i = 0;
            respuesta = Decimal.TryParse(cadena, out i);

            return respuesta;
        }

        public Boolean isDecimal(String Cadena)
        {
            decimal resul;
            return decimal.TryParse(Cadena, out resul);
        }
        
        public Boolean getValidarNCF(Boolean activarMensaje, String ncf)
        {
            Boolean respuesta = false;
            if (ncf.Length == 19)
            {
                return true;

            }
            else
            {
                respuesta = false;

            }
            return false;
        }
        
        public string getFormaFechaYYYYMMdd(DateTime fecha)
        {

            return fecha.ToString("yyyyMMdd");

        }

        public string getFormaFechaddMMYYY(DateTime fecha)
        {

            return fecha.ToString("ddMMyyyy");

        }

        public string getFormaFechaYYYYMM(DateTime fecha)
        {

            return fecha.ToString("yyyyMM");

        }

        public string getFormaFechaddMMyyyyHHmmss(DateTime fecha)
        {

            return fecha.ToString("dd-MM-dd-HH-mm-ss");

        }

        public string getFormaFechadd(DateTime fecha)
        {

            return fecha.ToString("dd");

        }

        public string getFormaFecha(DateTime fecha, string formato)
        {

            return fecha.ToString(formato);

        }

        public int GetNUmeroDiasRangoFechas(DateTime FechaInicio, DateTime FechaFin)
        {

            try
            {

                TimeSpan ts = FechaFin - FechaInicio;


                return ts.Days;
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return 0;

            }
            finally
            {
                //  Entity = null;

            }

        }

        public int GetRandon(int tamano)
        {
            var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            return new Random(seed).Next(0, tamano);

        }

        public String GetNumeroRandon(int tamano)
        {
            String Trama = "";
            int j = 0;
            for (int i = 0; i < 1000; i++)
            {
                int numero = GetRandon(9);

                if (j == 0)
                {
                    if (numero != 0)
                    {
                        Trama = Trama + numero;
                        j++;
                    }
                }

                else
                {
                    Trama = Trama + numero;
                    j++;
                }

                if (j == 6)
                {
                    break;
                }
            }

            return Trama;

        }

        public Boolean validarCorreoElectronico(string correo)
        {
            try
            {

                String sFormato;
                sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(correo, sFormato))
                {
                    if (Regex.Replace(correo, sFormato, String.Empty).Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validarCorreoElectronico.:" + ex.ToString());
                return false;
            }
        }

        public bool copiarPegarArchivo(string origPath, string destPath, bool overwrite)
        {
            try
            {
                if (Path.GetExtension(destPath) == "")
                {
                    destPath = Path.Combine(destPath, Path.GetFileName(origPath));
                }

                if (!File.Exists(destPath))
                {
                    File.Copy(origPath, destPath, true);
                }
                else
                {
                    if (overwrite == true)
                    {
                        EliminarArchivo(destPath);
                        File.Copy(origPath, destPath, true);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void validarTextBoxNumeroDecimal(KeyPressEventArgs e,string numeroCompleto)
        {
            if (numeroCompleto.ToString().Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        public void validarTextBoxNumeroEntero(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
        }
       
        public bool EliminarArchivo(string rutaArchivo)
        {
            try
            {
                //validar si el archivo no existe
                if (!File.Exists(rutaArchivo))
                {
                    return true;
                }
                File.Delete(rutaArchivo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void notificacionWindows(string titulo, string mensaje, int duracionSegundos, ToolTipIcon icono = ToolTipIcon.Info)
        {
            try
            {

                NotifyIcon notifyIcon=new NotifyIcon();
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipTitle = titulo;
                notifyIcon.Text = mensaje;
                notifyIcon.BalloonTipText = mensaje;
                duracionSegundos *= 1000;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(duracionSegundos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error notificacionWindows.:" + ex.ToString());
                
            }
        }

        public void getPalabraArchivoTxt(string rutaArchivo, string palabra, string delimitador = ";")
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getPalabraArchivoTxt.:" + ex.ToString());
            }
        }

        public string getFechaHoraMinutosSegundos(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("dd/MM/yyyy hh:mm:ss");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaHoraMinutosSegundos.:" + ex.ToString());
                return null;
            }
        }
        
        public string getFechaddMMyyyy(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("dd/MM/yyyy");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaddMMyyyy.:" + ex.ToString());
                return null;
            }
        }

        public string getFecha_dd_MM_yyyy(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("dd/MM/yyyy");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFecha_dd_MM_yyyy.:" + ex.ToString());
                return null;
            }
        }

        public string getFecha_dd_MM_yyyy_hh_mm_ss_tt(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("dd/MM/yyyy hh:mm:ss tt");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFecha_dd_MM_yyyy_hh_mm_ss_tt.:" + ex.ToString());
                return null;
            }
        }
        
        public string getFechaddMMyyyyhhmmsstt(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("ddMMyyyy hhmmsstt");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaddMMyyyyhhmmsstt.:" + ex.ToString());
                return null;
            }
        }
        
        public string getFechaddMM_yyyy_hh_mm_ss_tt(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("dd/MM/yyyy hh:mm:ss tt");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaddMM_yyyy_hh_mm_ss_tt.:" + ex.ToString());
                return null;
            }
        }

        public string getFechaMMyddyyyhhmmsstt(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("MM/dd/yyyy hh:mm:ss tt");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaddMMyyyyhhmmsstt.:" + ex.ToString());
                return null;
            }
        }

        public string getFecha_MM_dd_yyyy_hh_mm_ss_tt(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("MM/dd/yyyy hh:mm:ss tt");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFecha_MM_dd_yyyy_hh_mm_ss_tt.:" + ex.ToString());
                return null;
            }
        }
        
        public string getFechayyyyMMdd(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("yyyyMMdd");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechayyyyMMdd.:" + ex.ToString());
                return null;
            }
        }
        
        public string getFechayyyyMMddhhmmss(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("yyyyMMddhhmmss");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechayyyyMMddhhmmss.:" + ex.ToString());
                return null;
            }
        }

        public string getFechaMMddyyyy(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("MMddyyyy");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaMMddyyyy.:" + ex.ToString());
                return null;
            }
        }

        public string getFecha_MM_dd_yyyy(DateTime fecha)
        {
            try
            {
                string fechaConvertida = fecha.ToString("MM/dd/yyyy");
                return fechaConvertida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFecha_MM_dd_yyyy.:" + ex.ToString());
                return null;
            }
        }
        
        public string getDiasByRangoFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                TimeSpan ts = fechaFinal - fechaInicial;
                return ts.Days.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getDiasByRangoFecha.:" + ex.ToString());
                return null;
            }
        }

        public string getRellenar(string dato,char caracterRelleno,int numeroPosiciones,bool rellenarIzquierda=true)
        {
            try
            {
                if (rellenarIzquierda == false)
                {
                    //derecha
                    dato = dato.PadRight(numeroPosiciones, caracterRelleno);
                }
                else
                {
                    //izquierda
                    dato = dato.PadLeft(numeroPosiciones, caracterRelleno);
                }
                return dato;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getRellenar.:" + ex.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }

        //saber la diferencia de 2 fechas en dias
        public string getFechaDiferenciaDias(DateTime fechaVieja,DateTime fechaNueva)
        {
            try
            {
                // Difference in days, hours, and minutes.
                TimeSpan ts = fechaNueva - fechaVieja;

                // Difference in days.
                int differenceInDays = ts.Days;

                return differenceInDays.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getFechaDiferenciaDias.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public TextBox placeHolder(System.Windows.Forms.TextBox textBox, string texto, Color colorHolder,Color colorNoHolder)
        {
            try
            {
                if (String.IsNullOrEmpty(textBox.Text) || textBox.Text==texto)
                {
                    textBox.Text = texto;
                    textBox.ForeColor = colorHolder;
                }
                else
                {
                    //se queda el mismo texto pero se pone la letra en negrita
                    textBox.ForeColor = colorNoHolder;
                }

                return textBox;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en placeHolder.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }


    }
}
