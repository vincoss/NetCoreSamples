using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using System.Security;
using System.Collections.Concurrent;
using System.Net;
using System.Text;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Storage;
using Azure.ResourceManager;
using Azure.Security.KeyVault.Secrets;
using Azure.ResourceManager.KeyVault;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace Azure_ConsoleTest
{
	public class TestAzureHelper
	{
		public static AzureAppSettings AzureSettings = new AzureAppSettings
		{
		};

		public static IOptions<AzureAppSettings> AzureOptions = new TestOptions<AzureAppSettings>(AzureSettings);

		public static string KEYVAULT_BASE_URI = $"https://{AzureSettings.ValutId}.vault.azure.net";
	}

	public class TestOptions<T> : IOptions<T> where T : class, new()
	{
		public TestOptions(T value)
		{
			Value = value;
		}

		public T Value { get; }
	}

	public static class BlobDefaultTags
	{
		/// <summary>
		/// https://docs.microsoft.com/en-us/azure/storage/blobs/storage-manage-find-blobs?tabs=json
		/// https://regexrenamer.sourceforge.net/help/regex_quickref.html
		/// </summary>
		public static string SanitizeTagValue(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}
			var maxLength = 256;
			var pattern = @"[^a-zA-Z0-9_\/\+\-\.\:\=]";
			var str = Regex.Replace(value, pattern, "", RegexOptions.None);
			str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
			return str;
		}

		public static void SanitizeTags(IDictionary<string, string> tags)
		{
			for (var i = 0; i < tags.Count; i++)
			{
				var pair = tags.ElementAt(i);
				tags[pair.Key] = SanitizeTagValue(pair.Value);
			}
		}
	}

	public class AsyncHelper
	{
		private static readonly TaskFactory _taskFactory = new
		TaskFactory(CancellationToken.None,
					TaskCreationOptions.None,
					TaskContinuationOptions.None,
					TaskScheduler.Default);

		public static TResult RunSync<TResult>(Func<Task<TResult>> func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));

			return AsyncHelper._taskFactory
			  .StartNew<Task<TResult>>(func)
			  .Unwrap<TResult>()
			  .GetAwaiter()
			  .GetResult();
		}

		public static void RunSync(Func<Task> func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));

			AsyncHelper._taskFactory
			  .StartNew<Task>(func)
			  .Unwrap()
			  .GetAwaiter()
			  .GetResult();
		}
	}

	/// <summary>
	/// https://github.com/samuelneff/MimeTypeMap
	/// Class MimeTypeMap.
	/// </summary>
	public static class MimeTypeMap
	{
		private const string Dot = ".";
		private const string QuestionMark = "?";
		private const string DefaultMimeType = "application/octet-stream";
		private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);

		private static IDictionary<string, string> BuildMappings()
		{
			var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {

                #region Big freaking list of mime types
            
                // maps both ways,
                // extension -> mime type
                //   and
                // mime type -> extension
                //
                // any mime types on left side not pre-loaded on right side, are added automatically
                // some mime types can map to multiple extensions, so to get a deterministic mapping,
                // add those to the dictionary specifically
                //
                // combination of values from Windows 7 Registry and 
                // from C:\Windows\System32\inetsrv\config\applicationHost.config
                // some added, including .7z and .dat
                //
                // Some added based on http://www.iana.org/assignments/media-types/media-types.xhtml
                // which lists mime types, but not extensions
                //
                {".323", "text/h323"},
				{".3g2", "video/3gpp2"},
				{".3gp", "video/3gpp"},
				{".3gp2", "video/3gpp2"},
				{".3gpp", "video/3gpp"},
				{".7z", "application/x-7z-compressed"},
				{".aa", "audio/audible"},
				{".AAC", "audio/aac"},
				{".aaf", "application/octet-stream"},
				{".aax", "audio/vnd.audible.aax"},
				{".ac3", "audio/ac3"},
				{".aca", "application/octet-stream"},
				{".accda", "application/msaccess.addin"},
				{".accdb", "application/msaccess"},
				{".accdc", "application/msaccess.cab"},
				{".accde", "application/msaccess"},
				{".accdr", "application/msaccess.runtime"},
				{".accdt", "application/msaccess"},
				{".accdw", "application/msaccess.webapplication"},
				{".accft", "application/msaccess.ftemplate"},
				{".acx", "application/internet-property-stream"},
				{".AddIn", "text/xml"},
				{".ade", "application/msaccess"},
				{".adobebridge", "application/x-bridge-url"},
				{".adp", "application/msaccess"},
				{".ADT", "audio/vnd.dlna.adts"},
				{".ADTS", "audio/aac"},
				{".afm", "application/octet-stream"},
				{".ai", "application/postscript"},
				{".aif", "audio/aiff"},
				{".aifc", "audio/aiff"},
				{".aiff", "audio/aiff"},
				{".air", "application/vnd.adobe.air-application-installer-package+zip"},
				{".amc", "application/mpeg"},
				{".anx", "application/annodex"},
				{".apk", "application/vnd.android.package-archive"},
				{".apng", "image/apng"},
				{".application", "application/x-ms-application"},
				{".art", "image/x-jg"},
				{".asa", "application/xml"},
				{".asax", "application/xml"},
				{".ascx", "application/xml"},
				{".asd", "application/octet-stream"},
				{".asf", "video/x-ms-asf"},
				{".ashx", "application/xml"},
				{".asi", "application/octet-stream"},
				{".asm", "text/plain"},
				{".asmx", "application/xml"},
				{".aspx", "application/xml"},
				{".asr", "video/x-ms-asf"},
				{".asx", "video/x-ms-asf"},
				{".atom", "application/atom+xml"},
				{".au", "audio/basic"},
				{".avci", "image/avci"},
				{".avcs", "image/avcs"},
				{".avi", "video/x-msvideo"},
				{".avif", "image/avif"},
				{".avifs", "image/avif-sequence"},
				{".axa", "audio/annodex"},
				{".axs", "application/olescript"},
				{".axv", "video/annodex"},
				{".bas", "text/plain"},
				{".bcpio", "application/x-bcpio"},
				{".bin", "application/octet-stream"},
				{".bmp", "image/bmp"},
				{".c", "text/plain"},
				{".cab", "application/octet-stream"},
				{".caf", "audio/x-caf"},
				{".calx", "application/vnd.ms-office.calx"},
				{".cat", "application/vnd.ms-pki.seccat"},
				{".cc", "text/plain"},
				{".cd", "text/plain"},
				{".cdda", "audio/aiff"},
				{".cdf", "application/x-cdf"},
				{".cer", "application/x-x509-ca-cert"},
				{".cfg", "text/plain"},
				{".chm", "application/octet-stream"},
				{".class", "application/x-java-applet"},
				{".clp", "application/x-msclip"},
				{".cmd", "text/plain"},
				{".cmx", "image/x-cmx"},
				{".cnf", "text/plain"},
				{".cod", "image/cis-cod"},
				{".config", "application/xml"},
				{".contact", "text/x-ms-contact"},
				{".coverage", "application/xml"},
				{".cpio", "application/x-cpio"},
				{".cpp", "text/plain"},
				{".crd", "application/x-mscardfile"},
				{".crl", "application/pkix-crl"},
				{".crt", "application/x-x509-ca-cert"},
				{".cs", "text/plain"},
				{".csdproj", "text/plain"},
				{".csh", "application/x-csh"},
				{".csproj", "text/plain"},
				{".css", "text/css"},
				{".csv", "text/csv"},
				{".cur", "application/octet-stream"},
				{".czx", "application/x-czx"},
				{".cxx", "text/plain"},
				{".dat", "application/octet-stream"},
				{".datasource", "application/xml"},
				{".dbproj", "text/plain"},
				{".dcr", "application/x-director"},
				{".def", "text/plain"},
				{".deploy", "application/octet-stream"},
				{".der", "application/x-x509-ca-cert"},
				{".dgml", "application/xml"},
				{".dib", "image/bmp"},
				{".dif", "video/x-dv"},
				{".dir", "application/x-director"},
				{".disco", "text/xml"},
				{".divx", "video/divx"},
				{".dll", "application/x-msdownload"},
				{".dll.config", "text/xml"},
				{".dlm", "text/dlm"},
				{".doc", "application/msword"},
				{".docm", "application/vnd.ms-word.document.macroEnabled.12"},
				{".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
				{".dot", "application/msword"},
				{".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
				{".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
				{".dsp", "application/octet-stream"},
				{".dsw", "text/plain"},
				{".dtd", "text/xml"},
				{".dtsConfig", "text/xml"},
				{".dv", "video/x-dv"},
				{".dvi", "application/x-dvi"},
				{".dwf", "drawing/x-dwf"},
				{".dwg", "application/acad"},
				{".dwp", "application/octet-stream"},
				{".dxf", "application/x-dxf"},
				{".dxr", "application/x-director"},
				{".eml", "message/rfc822"},
				{".emf", "image/emf"},
				{".emz", "application/octet-stream"},
				{".eot", "application/vnd.ms-fontobject"},
				{".eps", "application/postscript"},
				{".es", "application/ecmascript"},
				{".etl", "application/etl"},
				{".etx", "text/x-setext"},
				{".evy", "application/envoy"},
				{".exe", "application/vnd.microsoft.portable-executable"},
				{".exe.config", "text/xml"},
				{".f4v", "video/mp4"},
				{".fdf", "application/vnd.fdf"},
				{".fif", "application/fractals"},
				{".filters", "application/xml"},
				{".fla", "application/octet-stream"},
				{".flac", "audio/flac"},
				{".flr", "x-world/x-vrml"},
				{".flv", "video/x-flv"},
				{".fsscript", "application/fsharp-script"},
				{".fsx", "application/fsharp-script"},
				{".generictest", "application/xml"},
				{".geojson", "application/geo+json"},
				{".gif", "image/gif"},
				{".gpx", "application/gpx+xml"},
				{".group", "text/x-ms-group"},
				{".gsm", "audio/x-gsm"},
				{".gtar", "application/x-gtar"},
				{".gz", "application/x-gzip"},
				{".h", "text/plain"},
				{".hdf", "application/x-hdf"},
				{".hdml", "text/x-hdml"},
				{".heic", "image/heic"},
				{".heics", "image/heic-sequence"},
				{".heif", "image/heif"},
				{".heifs", "image/heif-sequence"},
				{".hhc", "application/x-oleobject"},
				{".hhk", "application/octet-stream"},
				{".hhp", "application/octet-stream"},
				{".hlp", "application/winhlp"},
				{".hpp", "text/plain"},
				{".hqx", "application/mac-binhex40"},
				{".hta", "application/hta"},
				{".htc", "text/x-component"},
				{".htm", "text/html"},
				{".html", "text/html"},
				{".htt", "text/webviewhtml"},
				{".hxa", "application/xml"},
				{".hxc", "application/xml"},
				{".hxd", "application/octet-stream"},
				{".hxe", "application/xml"},
				{".hxf", "application/xml"},
				{".hxh", "application/octet-stream"},
				{".hxi", "application/octet-stream"},
				{".hxk", "application/xml"},
				{".hxq", "application/octet-stream"},
				{".hxr", "application/octet-stream"},
				{".hxs", "application/octet-stream"},
				{".hxt", "text/html"},
				{".hxv", "application/xml"},
				{".hxw", "application/octet-stream"},
				{".hxx", "text/plain"},
				{".i", "text/plain"},
				{".ical", "text/calendar"},
				{".icalendar", "text/calendar"},
				{".ico", "image/x-icon"},
				{".ics", "text/calendar"},
				{".idl", "text/plain"},
				{".ief", "image/ief"},
				{".ifb", "text/calendar"},
				{".iii", "application/x-iphone"},
				{".inc", "text/plain"},
				{".inf", "application/octet-stream"},
				{".ini", "text/plain"},
				{".inl", "text/plain"},
				{".ins", "application/x-internet-signup"},
				{".ipa", "application/x-itunes-ipa"},
				{".ipg", "application/x-itunes-ipg"},
				{".ipproj", "text/plain"},
				{".ipsw", "application/x-itunes-ipsw"},
				{".iqy", "text/x-ms-iqy"},
				{".isp", "application/x-internet-signup"},
				{".isma", "application/octet-stream"},
				{".ismv", "application/octet-stream"},
				{".ite", "application/x-itunes-ite"},
				{".itlp", "application/x-itunes-itlp"},
				{".itms", "application/x-itunes-itms"},
				{".itpc", "application/x-itunes-itpc"},
				{".IVF", "video/x-ivf"},
				{".jar", "application/java-archive"},
				{".java", "application/octet-stream"},
				{".jck", "application/liquidmotion"},
				{".jcz", "application/liquidmotion"},
				{".jfif", "image/pjpeg"},
				{".jnlp", "application/x-java-jnlp-file"},
				{".jpb", "application/octet-stream"},
				{".jpe", "image/jpeg"},
				{".jpeg", "image/jpeg"},
				{".jpg", "image/jpeg"},
				{".js", "application/javascript"},
				{".json", "application/json"},
				{".jsx", "text/jscript"},
				{".jsxbin", "text/plain"},
				{".latex", "application/x-latex"},
				{".library-ms", "application/windows-library+xml"},
				{".lit", "application/x-ms-reader"},
				{".loadtest", "application/xml"},
				{".lpk", "application/octet-stream"},
				{".lsf", "video/x-la-asf"},
				{".lst", "text/plain"},
				{".lsx", "video/x-la-asf"},
				{".lzh", "application/octet-stream"},
				{".m13", "application/x-msmediaview"},
				{".m14", "application/x-msmediaview"},
				{".m1v", "video/mpeg"},
				{".m2t", "video/vnd.dlna.mpeg-tts"},
				{".m2ts", "video/vnd.dlna.mpeg-tts"},
				{".m2v", "video/mpeg"},
				{".m3u", "audio/x-mpegurl"},
				{".m3u8", "audio/x-mpegurl"},
				{".m4a", "audio/m4a"},
				{".m4b", "audio/m4b"},
				{".m4p", "audio/m4p"},
				{".m4r", "audio/x-m4r"},
				{".m4v", "video/x-m4v"},
				{".mac", "image/x-macpaint"},
				{".mak", "text/plain"},
				{".man", "application/x-troff-man"},
				{".manifest", "application/x-ms-manifest"},
				{".map", "text/plain"},
				{".master", "application/xml"},
				{".mbox", "application/mbox"},
				{".mda", "application/msaccess"},
				{".mdb", "application/x-msaccess"},
				{".mde", "application/msaccess"},
				{".mdp", "application/octet-stream"},
				{".me", "application/x-troff-me"},
				{".mfp", "application/x-shockwave-flash"},
				{".mht", "message/rfc822"},
				{".mhtml", "message/rfc822"},
				{".mid", "audio/mid"},
				{".midi", "audio/mid"},
				{".mix", "application/octet-stream"},
				{".mk", "text/plain"},
				{".mk3d", "video/x-matroska-3d"},
				{".mka", "audio/x-matroska"},
				{".mkv", "video/x-matroska"},
				{".mmf", "application/x-smaf"},
				{".mno", "text/xml"},
				{".mny", "application/x-msmoney"},
				{".mod", "video/mpeg"},
				{".mov", "video/quicktime"},
				{".movie", "video/x-sgi-movie"},
				{".mp2", "video/mpeg"},
				{".mp2v", "video/mpeg"},
				{".mp3", "audio/mpeg"},
				{".mp4", "video/mp4"},
				{".mp4v", "video/mp4"},
				{".mpa", "video/mpeg"},
				{".mpe", "video/mpeg"},
				{".mpeg", "video/mpeg"},
				{".mpf", "application/vnd.ms-mediapackage"},
				{".mpg", "video/mpeg"},
				{".mpp", "application/vnd.ms-project"},
				{".mpv2", "video/mpeg"},
				{".mqv", "video/quicktime"},
				{".ms", "application/x-troff-ms"},
				{".msg", "application/vnd.ms-outlook"},
				{".msi", "application/octet-stream"},
				{".mso", "application/octet-stream"},
				{".mts", "video/vnd.dlna.mpeg-tts"},
				{".mtx", "application/xml"},
				{".mvb", "application/x-msmediaview"},
				{".mvc", "application/x-miva-compiled"},
				{".mxf", "application/mxf"},
				{".mxp", "application/x-mmxp"},
				{".nc", "application/x-netcdf"},
				{".nsc", "video/x-ms-asf"},
				{".nws", "message/rfc822"},
				{".ocx", "application/octet-stream"},
				{".oda", "application/oda"},
				{".odb", "application/vnd.oasis.opendocument.database"},
				{".odc", "application/vnd.oasis.opendocument.chart"},
				{".odf", "application/vnd.oasis.opendocument.formula"},
				{".odg", "application/vnd.oasis.opendocument.graphics"},
				{".odh", "text/plain"},
				{".odi", "application/vnd.oasis.opendocument.image"},
				{".odl", "text/plain"},
				{".odm", "application/vnd.oasis.opendocument.text-master"},
				{".odp", "application/vnd.oasis.opendocument.presentation"},
				{".ods", "application/vnd.oasis.opendocument.spreadsheet"},
				{".odt", "application/vnd.oasis.opendocument.text"},
				{".oga", "audio/ogg"},
				{".ogg", "audio/ogg"},
				{".ogv", "video/ogg"},
				{".ogx", "application/ogg"},
				{".one", "application/onenote"},
				{".onea", "application/onenote"},
				{".onepkg", "application/onenote"},
				{".onetmp", "application/onenote"},
				{".onetoc", "application/onenote"},
				{".onetoc2", "application/onenote"},
				{".opus", "audio/ogg"},
				{".orderedtest", "application/xml"},
				{".osdx", "application/opensearchdescription+xml"},
				{".otf", "application/font-sfnt"},
				{".otg", "application/vnd.oasis.opendocument.graphics-template"},
				{".oth", "application/vnd.oasis.opendocument.text-web"},
				{".otp", "application/vnd.oasis.opendocument.presentation-template"},
				{".ots", "application/vnd.oasis.opendocument.spreadsheet-template"},
				{".ott", "application/vnd.oasis.opendocument.text-template"},
				{".oxps", "application/oxps"},
				{".oxt", "application/vnd.openofficeorg.extension"},
				{".p10", "application/pkcs10"},
				{".p12", "application/x-pkcs12"},
				{".p7b", "application/x-pkcs7-certificates"},
				{".p7c", "application/pkcs7-mime"},
				{".p7m", "application/pkcs7-mime"},
				{".p7r", "application/x-pkcs7-certreqresp"},
				{".p7s", "application/pkcs7-signature"},
				{".pbm", "image/x-portable-bitmap"},
				{".pcast", "application/x-podcast"},
				{".pct", "image/pict"},
				{".pcx", "application/octet-stream"},
				{".pcz", "application/octet-stream"},
				{".pdf", "application/pdf"},
				{".pfb", "application/octet-stream"},
				{".pfm", "application/octet-stream"},
				{".pfx", "application/x-pkcs12"},
				{".pgm", "image/x-portable-graymap"},
				{".pic", "image/pict"},
				{".pict", "image/pict"},
				{".pkgdef", "text/plain"},
				{".pkgundef", "text/plain"},
				{".pko", "application/vnd.ms-pki.pko"},
				{".pls", "audio/scpls"},
				{".pma", "application/x-perfmon"},
				{".pmc", "application/x-perfmon"},
				{".pml", "application/x-perfmon"},
				{".pmr", "application/x-perfmon"},
				{".pmw", "application/x-perfmon"},
				{".png", "image/png"},
				{".pnm", "image/x-portable-anymap"},
				{".pnt", "image/x-macpaint"},
				{".pntg", "image/x-macpaint"},
				{".pnz", "image/png"},
				{".pot", "application/vnd.ms-powerpoint"},
				{".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
				{".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
				{".ppa", "application/vnd.ms-powerpoint"},
				{".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
				{".ppm", "image/x-portable-pixmap"},
				{".pps", "application/vnd.ms-powerpoint"},
				{".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
				{".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
				{".ppt", "application/vnd.ms-powerpoint"},
				{".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
				{".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
				{".prf", "application/pics-rules"},
				{".prm", "application/octet-stream"},
				{".prx", "application/octet-stream"},
				{".ps", "application/postscript"},
				{".psc1", "application/PowerShell"},
				{".psd", "application/octet-stream"},
				{".psess", "application/xml"},
				{".psm", "application/octet-stream"},
				{".psp", "application/octet-stream"},
				{".pst", "application/vnd.ms-outlook"},
				{".pub", "application/x-mspublisher"},
				{".pwz", "application/vnd.ms-powerpoint"},
				{".qht", "text/x-html-insertion"},
				{".qhtm", "text/x-html-insertion"},
				{".qt", "video/quicktime"},
				{".qti", "image/x-quicktime"},
				{".qtif", "image/x-quicktime"},
				{".qtl", "application/x-quicktimeplayer"},
				{".qxd", "application/octet-stream"},
				{".ra", "audio/x-pn-realaudio"},
				{".ram", "audio/x-pn-realaudio"},
				{".rar", "application/x-rar-compressed"},
				{".ras", "image/x-cmu-raster"},
				{".rat", "application/rat-file"},
				{".rc", "text/plain"},
				{".rc2", "text/plain"},
				{".rct", "text/plain"},
				{".rdlc", "application/xml"},
				{".reg", "text/plain"},
				{".resx", "application/xml"},
				{".rf", "image/vnd.rn-realflash"},
				{".rgb", "image/x-rgb"},
				{".rgs", "text/plain"},
				{".rm", "application/vnd.rn-realmedia"},
				{".rmi", "audio/mid"},
				{".rmp", "application/vnd.rn-rn_music_package"},
				{".rmvb", "application/vnd.rn-realmedia-vbr"},
				{".roff", "application/x-troff"},
				{".rpm", "audio/x-pn-realaudio-plugin"},
				{".rqy", "text/x-ms-rqy"},
				{".rtf", "application/rtf"},
				{".rtx", "text/richtext"},
				{".rvt", "application/octet-stream"},
				{".ruleset", "application/xml"},
				{".s", "text/plain"},
				{".safariextz", "application/x-safari-safariextz"},
				{".scd", "application/x-msschedule"},
				{".scr", "text/plain"},
				{".sct", "text/scriptlet"},
				{".sd2", "audio/x-sd2"},
				{".sdp", "application/sdp"},
				{".sea", "application/octet-stream"},
				{".searchConnector-ms", "application/windows-search-connector+xml"},
				{".setpay", "application/set-payment-initiation"},
				{".setreg", "application/set-registration-initiation"},
				{".settings", "application/xml"},
				{".sgimb", "application/x-sgimb"},
				{".sgml", "text/sgml"},
				{".sh", "application/x-sh"},
				{".shar", "application/x-shar"},
				{".shtml", "text/html"},
				{".sit", "application/x-stuffit"},
				{".sitemap", "application/xml"},
				{".skin", "application/xml"},
				{".skp", "application/x-koan"},
				{".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
				{".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
				{".slk", "application/vnd.ms-excel"},
				{".sln", "text/plain"},
				{".slupkg-ms", "application/x-ms-license"},
				{".smd", "audio/x-smd"},
				{".smi", "application/octet-stream"},
				{".smx", "audio/x-smd"},
				{".smz", "audio/x-smd"},
				{".snd", "audio/basic"},
				{".snippet", "application/xml"},
				{".snp", "application/octet-stream"},
				{".sql", "application/sql"},
				{".sol", "text/plain"},
				{".sor", "text/plain"},
				{".spc", "application/x-pkcs7-certificates"},
				{".spl", "application/futuresplash"},
				{".spx", "audio/ogg"},
				{".src", "application/x-wais-source"},
				{".srf", "text/plain"},
				{".SSISDeploymentManifest", "text/xml"},
				{".ssm", "application/streamingmedia"},
				{".sst", "application/vnd.ms-pki.certstore"},
				{".stl", "application/vnd.ms-pki.stl"},
				{".sv4cpio", "application/x-sv4cpio"},
				{".sv4crc", "application/x-sv4crc"},
				{".svc", "application/xml"},
				{".svg", "image/svg+xml"},
				{".swf", "application/x-shockwave-flash"},
				{".step", "application/step"},
				{".stp", "application/step"},
				{".t", "application/x-troff"},
				{".tar", "application/x-tar"},
				{".tcl", "application/x-tcl"},
				{".testrunconfig", "application/xml"},
				{".testsettings", "application/xml"},
				{".tex", "application/x-tex"},
				{".texi", "application/x-texinfo"},
				{".texinfo", "application/x-texinfo"},
				{".tgz", "application/x-compressed"},
				{".thmx", "application/vnd.ms-officetheme"},
				{".thn", "application/octet-stream"},
				{".tif", "image/tiff"},
				{".tiff", "image/tiff"},
				{".tlh", "text/plain"},
				{".tli", "text/plain"},
				{".toc", "application/octet-stream"},
				{".tr", "application/x-troff"},
				{".trm", "application/x-msterminal"},
				{".trx", "application/xml"},
				{".ts", "video/vnd.dlna.mpeg-tts"},
				{".tsv", "text/tab-separated-values"},
				{".ttf", "application/font-sfnt"},
				{".tts", "video/vnd.dlna.mpeg-tts"},
				{".txt", "text/plain"},
				{".u32", "application/octet-stream"},
				{".uls", "text/iuls"},
				{".user", "text/plain"},
				{".ustar", "application/x-ustar"},
				{".vb", "text/plain"},
				{".vbdproj", "text/plain"},
				{".vbk", "video/mpeg"},
				{".vbproj", "text/plain"},
				{".vbs", "text/vbscript"},
				{".vcf", "text/x-vcard"},
				{".vcproj", "application/xml"},
				{".vcs", "text/plain"},
				{".vcxproj", "application/xml"},
				{".vddproj", "text/plain"},
				{".vdp", "text/plain"},
				{".vdproj", "text/plain"},
				{".vdx", "application/vnd.ms-visio.viewer"},
				{".vml", "text/xml"},
				{".vscontent", "application/xml"},
				{".vsct", "text/xml"},
				{".vsd", "application/vnd.visio"},
				{".vsi", "application/ms-vsi"},
				{".vsix", "application/vsix"},
				{".vsixlangpack", "text/xml"},
				{".vsixmanifest", "text/xml"},
				{".vsmdi", "application/xml"},
				{".vspscc", "text/plain"},
				{".vss", "application/vnd.visio"},
				{".vsscc", "text/plain"},
				{".vssettings", "text/xml"},
				{".vssscc", "text/plain"},
				{".vst", "application/vnd.visio"},
				{".vstemplate", "text/xml"},
				{".vsto", "application/x-ms-vsto"},
				{".vsw", "application/vnd.visio"},
				{".vsx", "application/vnd.visio"},
				{".vtt", "text/vtt"},
				{".vtx", "application/vnd.visio"},
				{".wasm", "application/wasm"},
				{".wav", "audio/wav"},
				{".wave", "audio/wav"},
				{".wax", "audio/x-ms-wax"},
				{".wbk", "application/msword"},
				{".wbmp", "image/vnd.wap.wbmp"},
				{".wcm", "application/vnd.ms-works"},
				{".wdb", "application/vnd.ms-works"},
				{".wdp", "image/vnd.ms-photo"},
				{".webarchive", "application/x-safari-webarchive"},
				{".webm", "video/webm"},
				{".webp", "image/webp"}, /* https://en.wikipedia.org/wiki/WebP */
                {".webtest", "application/xml"},
				{".wiq", "application/xml"},
				{".wiz", "application/msword"},
				{".wks", "application/vnd.ms-works"},
				{".WLMP", "application/wlmoviemaker"},
				{".wlpginstall", "application/x-wlpg-detect"},
				{".wlpginstall3", "application/x-wlpg3-detect"},
				{".wm", "video/x-ms-wm"},
				{".wma", "audio/x-ms-wma"},
				{".wmd", "application/x-ms-wmd"},
				{".wmf", "application/x-msmetafile"},
				{".wml", "text/vnd.wap.wml"},
				{".wmlc", "application/vnd.wap.wmlc"},
				{".wmls", "text/vnd.wap.wmlscript"},
				{".wmlsc", "application/vnd.wap.wmlscriptc"},
				{".wmp", "video/x-ms-wmp"},
				{".wmv", "video/x-ms-wmv"},
				{".wmx", "video/x-ms-wmx"},
				{".wmz", "application/x-ms-wmz"},
				{".woff", "application/font-woff"},
				{".woff2", "application/font-woff2"},
				{".wpl", "application/vnd.ms-wpl"},
				{".wps", "application/vnd.ms-works"},
				{".wri", "application/x-mswrite"},
				{".wrl", "x-world/x-vrml"},
				{".wrz", "x-world/x-vrml"},
				{".wsc", "text/scriptlet"},
				{".wsdl", "text/xml"},
				{".wvx", "video/x-ms-wvx"},
				{".x", "application/directx"},
				{".xaf", "x-world/x-vrml"},
				{".xaml", "application/xaml+xml"},
				{".xap", "application/x-silverlight-app"},
				{".xbap", "application/x-ms-xbap"},
				{".xbm", "image/x-xbitmap"},
				{".xdr", "text/plain"},
				{".xht", "application/xhtml+xml"},
				{".xhtml", "application/xhtml+xml"},
				{".xla", "application/vnd.ms-excel"},
				{".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
				{".xlc", "application/vnd.ms-excel"},
				{".xld", "application/vnd.ms-excel"},
				{".xlk", "application/vnd.ms-excel"},
				{".xll", "application/vnd.ms-excel"},
				{".xlm", "application/vnd.ms-excel"},
				{".xls", "application/vnd.ms-excel"},
				{".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
				{".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
				{".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
				{".xlt", "application/vnd.ms-excel"},
				{".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
				{".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
				{".xlw", "application/vnd.ms-excel"},
				{".xml", "text/xml"},
				{".xmp", "application/octet-stream"},
				{".xmta", "application/xml"},
				{".xof", "x-world/x-vrml"},
				{".XOML", "text/plain"},
				{".xpm", "image/x-xpixmap"},
				{".xps", "application/vnd.ms-xpsdocument"},
				{".xrm-ms", "text/xml"},
				{".xsc", "application/xml"},
				{".xsd", "text/xml"},
				{".xsf", "text/xml"},
				{".xsl", "text/xml"},
				{".xslt", "text/xml"},
				{".xsn", "application/octet-stream"},
				{".xss", "application/xml"},
				{".xspf", "application/xspf+xml"},
				{".xtp", "application/octet-stream"},
				{".xwd", "image/x-xwindowdump"},
				{".z", "application/x-compress"},
				{".zip", "application/zip"},

				{"application/fsharp-script", ".fsx"},
				{"application/msaccess", ".adp"},
				{"application/msword", ".doc"},
				{"application/octet-stream", ".bin"},
				{"application/onenote", ".one"},
				{"application/postscript", ".eps"},
				{"application/step", ".step"},
				{"application/vnd.ms-excel", ".xls"},
				{"application/vnd.ms-powerpoint", ".ppt"},
				{"application/vnd.ms-works", ".wks"},
				{"application/vnd.visio", ".vsd"},
				{"application/x-director", ".dir"},
				{"application/x-msdos-program", ".exe"},
				{"application/x-shockwave-flash", ".swf"},
				{"application/x-x509-ca-cert", ".cer"},
				{"application/x-zip-compressed", ".zip"},
				{"application/xhtml+xml", ".xhtml"},
				{"application/xml", ".xml"}, // anomaly, .xml -> text/xml, but application/xml -> many things, but all are xml, so safest is .xml
                {"audio/aac", ".AAC"},
				{"audio/aiff", ".aiff"},
				{"audio/basic", ".snd"},
				{"audio/mid", ".midi"},
				{"audio/mp4", ".m4a"}, // one way mapping only, mime -> ext
                {"audio/wav", ".wav"},
				{"audio/x-m4a", ".m4a"},
				{"audio/x-mpegurl", ".m3u"},
				{"audio/x-pn-realaudio", ".ra"},
				{"audio/x-smd", ".smd"},
				{"image/bmp", ".bmp"},
				{"image/jpeg", ".jpg"},
				{"image/pict", ".pic"},
				{"image/png", ".png"}, // Defined in [RFC-2045], [RFC-2048]
                {"image/x-png", ".png"}, // See https://www.w3.org/TR/PNG/#A-Media-type :"It is recommended that implementations also recognize the media type "image/x-png"."
                {"image/tiff", ".tiff"},
				{"image/x-macpaint", ".mac"},
				{"image/x-quicktime", ".qti"},
				{"message/rfc822", ".eml"},
				{"text/calendar", ".ics"},
				{"text/html", ".html"},
				{"text/plain", ".txt"},
				{"text/scriptlet", ".wsc"},
				{"text/xml", ".xml"},
				{"video/3gpp", ".3gp"},
				{"video/3gpp2", ".3gp2"},
				{"video/mp4", ".mp4"},
				{"video/mpeg", ".mpg"},
				{"video/quicktime", ".mov"},
				{"video/vnd.dlna.mpeg-tts", ".m2t"},
				{"video/x-dv", ".dv"},
				{"video/x-la-asf", ".lsf"},
				{"video/x-ms-asf", ".asf"},
				{"x-world/x-vrml", ".xof"},

                #endregion

                };

			var cache = mappings.ToList(); // need ToList() to avoid modifying while still enumerating

			foreach (var mapping in cache)
			{
				if (!mappings.ContainsKey(mapping.Value))
				{
					mappings.Add(mapping.Value, mapping.Key);
				}
			}

			return mappings;
		}

		/// <summary>
		/// Tries to get the type of the MIME from the provided string.
		/// </summary>
		/// <param name="str">The filename or extension.</param>
		/// <param name="mimeType">The variable to store the MIME type.</param>
		/// <returns>The MIME type.</returns>
		/// <exception cref="ArgumentNullException" />
		public static bool TryGetMimeType(string str, out string mimeType)
		{
			if (str == null)
			{
				throw new ArgumentNullException(nameof(str));
			}

			var indexQuestionMark = str.IndexOf(QuestionMark, StringComparison.Ordinal);
			if (indexQuestionMark != -1)
			{
				str = str.Remove(indexQuestionMark);
			}


			if (!str.StartsWith(Dot))
			{
				var index = str.LastIndexOf(Dot);
				if (index != -1 && str.Length > index + 1)
				{
					str = str.Substring(index + 1);
				}

				str = Dot + str;
			}

			return _mappings.Value.TryGetValue(str, out mimeType);
		}

		/// <summary>
		/// Gets the type of the MIME from the provided string.
		/// </summary>
		/// <param name="str">The filename or extension.</param>
		/// <returns>The MIME type.</returns>
		/// <exception cref="ArgumentNullException" />
		public static string GetMimeType(string str)
		{
			return MimeTypeMap.TryGetMimeType(str, out var result) ? result : DefaultMimeType;
		}

		/// <summary>
		/// Gets the extension from the provided MINE type.
		/// </summary>
		/// <param name="mimeType">Type of the MIME.</param>
		/// <param name="throwErrorIfNotFound">if set to <c>true</c>, throws error if extension's not found.</param>
		/// <returns>The extension.</returns>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="ArgumentException" />
		public static string GetExtension(string mimeType, bool throwErrorIfNotFound = true)
		{
			if (mimeType == null)
			{
				throw new ArgumentNullException(nameof(mimeType));
			}

			if (mimeType.StartsWith(Dot))
			{
				throw new ArgumentException("Requested mime type is not valid: " + mimeType);
			}

			if (_mappings.Value.TryGetValue(mimeType, out string extension))
			{
				return extension;
			}

			if (throwErrorIfNotFound)
			{
				throw new ArgumentException("Requested mime type is not registered: " + mimeType);
			}

			return string.Empty;
		}
	}

	public static class Extensions
	{
		public static SecureString ToSecureString(this string value)
		{
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

			var secureString = new SecureString();
			foreach (var c in value)
			{
				secureString.AppendChar(c);
			}

			return secureString;
		}

		public static string ToStringFromSecureString(this SecureString value)
		{
			IntPtr valuePtr = IntPtr.Zero;
			try
			{
				valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
				return Marshal.PtrToStringUni(valuePtr);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
			}
		}
	}

	public class AzureAppSettings
	{
		public string CliendId { get; set; }
		public string TenantId { get; set; }
		public string Secret { get; set; }
		public string ValutId { get; set; }
	}
	public class BlobDto
	{
		public string ContainerName { get; set; }
		public string BlobName { get; set; }

		public override string ToString()
		{
			return $"Container: {ContainerName}, BlobName: {BlobName}";
		}
	}

	public interface IAzureConfigurationService
	{
		Task InitialiseAzure();
		IEnumerable<string> GetKeys();
		Task<string> GetSecretAsync(string key);
		string GetStorageKey(string storageName);

		BlobServiceClient GetStorage(string organization);
		string GetBlobUri(string storageAccountName, string path);
		Task<QueueClient> GetQueue(string queue);

		string BuildStorageKey(string organization);
		string BuildSqlServerNameKey(string organization);
		string BuildSqlServerPasswordKey(string organization);
		string BuildDataSource(string serverName);

		string GetStorageConnectionString(string organization);

		Uri GetSasUriRead(string organization, string fileName, string displayName, string containerName);
		Uri GetSasUriCreate(string organization, string fileName, string displayName, string containerName);
		Uri GetBlobSasUriCreate(string organization, string containerName);

		Task<bool> BlobFileExistsAsync(string organization, string containerName, string blobName, CancellationToken cancellationToken);

		Task<Stream> DownloadFileAsync(string organization, string containerName, string blobName);
		Task<IEnumerable<BlobDto>> FindBlobItemsAsync(string organization, string containerName, IDictionary<string, string> tags);
		Task SetBlobTagsAsync(string organization, string containerName, string blobName, IDictionary<string, string> tags);

		Task<string> UploadFileAsync(string organization, string containerName, Guid newFileId, string originalFilePath, Stream fileToUpload, IDictionary<string, string> tags);
	}


	public class AzureConfigurationService : IAzureConfigurationService
	{
		private readonly IOptions<AzureAppSettings> _settings;
		private readonly ILogger<AzureConfigurationService> _logger;

		private readonly ConcurrentDictionary<string, SecureString> Cache = new ConcurrentDictionary<string, SecureString>(StringComparer.OrdinalIgnoreCase);

		public AzureConfigurationService(IOptions<AzureAppSettings> options, ILogger<AzureConfigurationService> logger)
		{
			_settings = options ?? throw new ArgumentNullException(nameof(options));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void Validate()
		{
			// Skip looks run in azure managed service identity.
			if (string.IsNullOrWhiteSpace(_settings.Value.TenantId))
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(_settings.Value.CliendId))
				throw new ArgumentNullException($"Client id is required: {nameof(_settings.Value.CliendId)}");
			if (string.IsNullOrWhiteSpace(_settings.Value.TenantId))
				throw new ArgumentNullException($"TenantId id is required: {nameof(_settings.Value.TenantId)}");
			if (string.IsNullOrWhiteSpace(_settings.Value.Secret))
				throw new ArgumentNullException($"Secret id is required: {nameof(_settings.Value.Secret)}");
			if (string.IsNullOrWhiteSpace(_settings.Value.ValutId))
				throw new ArgumentNullException($"ValutId id is required: {nameof(_settings.Value.ValutId)}");
		}

		public async Task InitialiseAzure()
		{
			Validate();
			var valut = GetValut();
			var secrets = valut.GetPropertiesOfSecretsAsync();

			await foreach (var item in secrets)
			{
				var secret = await GetSecretAsync(item.Name);
				var ss = secret.ToSecureString();
				Cache.TryAdd(item.Name, ss);
			}
		}

		public Task<string> GetSecretAsync(string key)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

			key = key.ToLower();

			var secure = Cache.GetOrAdd(key, (key) =>
			{
				Validate();
				var valut = GetValut();
				var secret = GetSecretAsync(valut, key);
				var ss = secret.ToSecureString();
				return ss;
			});

			return Task.FromResult(secure.ToStringFromSecureString());
		}

		private SecretClient GetValut()
		{
			var azure = GetAzure();
			var secretClient = GetValut(azure, _settings.Value.ValutId);
			return secretClient;
		}

		private static SecretClient GetValut(TokenCredential tokenCredential, string? vaultName)
		{
			var client = new ArmClient(tokenCredential);
			var subscription = client.GetDefaultSubscription();
			var vaults = subscription.GetKeyVaults();

			if (vaults.Any() == false)
			{
				throw new InvalidOperationException($"There are not vaults for default subscription: {subscription.Id}");
			}

			SecretClient? secretClient = null;
			var template = "https://{0}.vault.azure.net";
			vaultName = vaultName?.ToLower();

			if (string.IsNullOrWhiteSpace(vaultName))
			{
				var kvUri = string.Format(template, vaults.First().Data.Name);
				secretClient = new SecretClient(new Uri(kvUri), tokenCredential);
			}
			else
			{
				var v = vaults.Where(x => x.Data.Name == vaultName).FirstOrDefault();
				if (v != null)
				{
					var kvUri = string.Format(template, v.Data.Name);
					secretClient = new SecretClient(new Uri(kvUri), tokenCredential);
				}
			}

			if (secretClient == null)
			{
				throw new InvalidOperationException($"Could not find valut: {vaultName} for default subscription: {subscription.Id}");
			}

			return secretClient;
		}

		private string GetSecretAsync(SecretClient vault, string key)
		{
			var secret = vault.GetSecret(key);
			return secret.Value.Value;
		}

		public BlobServiceClient GetStorage(string organization)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));

			var connectionString = GetStorageConnectionString(organization);
			var client = new BlobServiceClient(connectionString);
			return client;
		}

		public string GetBlobUri(string storageAccountName, string path)
		{
			return $"https://{storageAccountName}.blob.core.windows.net/{path}".Trim(new[] { '/' }).ToLower();
		}

		public Task<QueueClient> GetQueue(string queue)
		{
			throw new NotImplementedException();
		}

		public string GetStorageKey(string storageName)
		{
			if (string.IsNullOrWhiteSpace(storageName)) throw new ArgumentNullException(nameof(storageName));

			storageName = storageName.ToLower();

			var secure = Cache.GetOrAdd(storageName, (key) =>
			{
				Validate();

				var credential = GetAzure();
				var storageAccount = GetStorageAccount(credential, storageName);

				var sk = storageAccount.GetKeys().First().Value;
				var ss = sk.ToSecureString();
				return ss;
			});

			return secure.ToStringFromSecureString();
		}

		private static StorageAccountResource GetStorageAccount(TokenCredential tokenCredential, string? storageName)
		{
			if (string.IsNullOrWhiteSpace(storageName)) throw new ArgumentNullException(nameof(storageName));

			var client = new ArmClient(tokenCredential);
			var subscription = client.GetDefaultSubscription();
			var storages = subscription.GetStorageAccounts();
			storageName = storageName?.ToLower();

			if (storages.Any() == false)
			{
				throw new InvalidOperationException($"There are not storage accounts for default subscription: {subscription.Id}");
			}

			var account = storages.Where(x => x.Data.Name == storageName).FirstOrDefault();

			if (account == null)
			{
				throw new InvalidOperationException($"Could not find storage account: {storageName} for default subscription: {subscription.Id}. Please review permissions for managed identity on an existing VM. ");
			}

			return account;
		}

		private TokenCredential GetAzure()
		{
			var clientId = _settings.Value.CliendId;
			var clientSecret = _settings.Value.Secret;
			var tenantId = _settings.Value.TenantId;

			/*
                NOTE: If runned locally need to provide azure cred to authenticate since not running under azure environment. 
            */

			/*
                NOTE: If runned locally need to provide azure cred to authenticate since not running under azure environment. 
            */

			TokenCredential? credentials = null;

			if (string.IsNullOrWhiteSpace(tenantId))
			{
				credentials = new ManagedIdentityCredential();
			}
			else
			{
				credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);
			}

			return credentials;
		}

		public string GetStorageConnectionString(string organization)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));

			var storageKey = BuildStorageKey(organization);
			var accountName = AsyncHelper.RunSync(() => GetSecretAsync(storageKey));
			var accountKey = GetStorageKey(accountName);

			return $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net";
		}

		public string BuildStorageKey(string organization)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));

			var value = $"stg-{organization.ToLower()}";
			return value;
		}

		public string BuildSqlServerNameKey(string organization)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));

			var value = $"sql-{organization.ToLower()}";
			return value;
		}

		public string BuildSqlServerPasswordKey(string organization)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));

			var value = $"sql-{organization.ToLower()}-password";
			return value;
		}

		public string BuildDataSource(string serverName)
		{
			if (string.IsNullOrWhiteSpace(serverName)) throw new ArgumentNullException(nameof(serverName));

			var value = $"{serverName.ToLower()}.database.windows.net";
			return value;
		}

		public IEnumerable<string> GetKeys()
		{
			return Cache.Keys.ToArray();
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/azure/storage/blobs/sas-service-create?tabs=dotnet
		/// </summary>
		public Uri GetSasUriRead(string organization, string fileName, string displayName, string containerName)
		{
			return GetSasUri(organization, fileName, displayName, containerName, BlobContainerSasPermissions.Read);
		}

		public Uri GetSasUriCreate(string organization, string fileName, string displayName, string containerName)
		{
			return GetSasUri(organization, fileName, displayName, containerName, BlobContainerSasPermissions.Create);
		}

		public Uri GetBlobSasUriCreate(string organization, string containerName)
		{
			return GetBlobSasUri(organization, containerName, BlobContainerSasPermissions.Write);
		}

		private Uri GetSasUri(string organization, string fileId, string displayName, string containerName, BlobContainerSasPermissions permissions)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));
			if (string.IsNullOrWhiteSpace(fileId)) throw new ArgumentNullException(nameof(fileId));
			if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));

			if (string.IsNullOrWhiteSpace(displayName))
			{
				displayName = fileId;
			}

			var connectionString = GetStorageConnectionString(organization);
			var client = new BlobServiceClient(connectionString);
			var container = client.GetBlobContainerClient(containerName.ToLower());
			var blob = container.GetBlobClient(fileId.ToLower());

			var sasBuilder = new BlobSasBuilder()
			{
				BlobContainerName = container.Name,
				Resource = "c",
				ContentDisposition = $"attachment; filename={displayName.ToLower()}"
			};

			sasBuilder.ExpiresOn = DateTime.UtcNow.AddMinutes(5);
			sasBuilder.SetPermissions(permissions);

			return blob.GenerateSasUri(sasBuilder);
		}

		private Uri GetBlobSasUri(string organization, string containerName, BlobContainerSasPermissions permissions)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));
			if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));

			var connectionString = GetStorageConnectionString(organization);
			var client = new BlobServiceClient(connectionString);
			var container = client.GetBlobContainerClient(containerName.ToLower());

			return container.GenerateSasUri(permissions, DateTimeOffset.UtcNow.AddMinutes(180));
		}

		public async Task<bool> BlobFileExistsAsync(string organization, string containerName, string blobName, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));
			if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));
			if (string.IsNullOrWhiteSpace(blobName)) throw new ArgumentNullException(nameof(blobName));

			var client = GetStorage(organization).GetBlobContainerClient(containerName);
			var blob = client.GetBlobClient(blobName);

			return await blob.ExistsAsync(cancellationToken);
		}

		public async Task<Stream> DownloadFileAsync(string organization, string containerName, string blobName)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));
			if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));
			if (string.IsNullOrWhiteSpace(blobName)) throw new ArgumentNullException(nameof(blobName));

			var client = GetStorage(organization).GetBlobContainerClient(containerName);
			var blob = client.GetBlobClient(blobName);
			if (!await blob.ExistsAsync())
			{
				throw new FileNotFoundException($"File not found. Organization: {organization}, Container: {containerName}, BlobName: {blobName}");
			}
			return await blob.OpenReadAsync(new Azure.Storage.Blobs.Models.BlobOpenReadOptions(false));
		}

		public async Task<IEnumerable<BlobDto>> FindBlobItemsAsync(string organization, string containerName, IDictionary<string, string> tags)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));

			if (tags == null)
			{
				tags = new Dictionary<string, string>();
			}

			var sb = new StringBuilder();

			if (string.IsNullOrWhiteSpace(containerName) == false)
			{
				sb.AppendLine($"@container='{containerName}'");
			}

			foreach (var tag in tags)
			{
				if (sb.Length > 0)
				{
					sb.AppendLine("AND");
				}

				sb.AppendLine($"{tag.Key}='{tag.Value}'");
			}

			var _client = GetStorage(organization);
			var foundItems = new List<TaggedBlobItem>();
			var filter = sb.ToString();

			await foreach (var page in _client.FindBlobsByTagsAsync(filter).AsPages())
			{
				foundItems.AddRange(page.Values);
			}

			var files = new List<BlobDto>();
			foreach (var item in foundItems)
			{
				files.Add(new BlobDto { ContainerName = item.BlobContainerName, BlobName = item.BlobName });
			}
			return files;
		}

		public async Task SetBlobTagsAsync(string organization, string containerName, string blobName, IDictionary<string, string> updateTags)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));
			if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));
			if (string.IsNullOrWhiteSpace(blobName)) throw new ArgumentNullException(nameof(blobName));
			if (updateTags == null) throw new ArgumentNullException(nameof(updateTags));

			var client = GetStorage(organization).GetBlobContainerClient(containerName);
			var blob = client.GetBlobClient(blobName);
			var tagsResult = await blob.GetTagsAsync();
			var tags = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			// Copy existing ones.
			if (tagsResult != null && tagsResult.Value != null)
			{
				foreach (var pair in tagsResult.Value.Tags)
				{
					tags.Add(pair.Key, pair.Value);
				}
			}

			// Update existing tags, and add new if not exits.
			foreach (var pair in updateTags)
			{
				if (tags.ContainsKey(pair.Key) == false)
				{
					tags.Add(pair.Key, pair.Value);
				}
				tags[pair.Key] = pair.Value;
			}

			BlobDefaultTags.SanitizeTags(tags);
			await blob.SetMetadataAsync(tags);
			await blob.SetTagsAsync(tags);
		}

		public async Task<string> UploadFileAsync(string organization, string containerName, Guid newFileId, string originalFilePath, Stream fileToUpload, IDictionary<string, string> tags)
		{
			if (string.IsNullOrWhiteSpace(organization)) throw new ArgumentNullException(nameof(organization));
			if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));
			if (newFileId == Guid.Empty) throw new ArgumentNullException(nameof(newFileId));
			if (string.IsNullOrWhiteSpace(originalFilePath)) throw new ArgumentNullException(nameof(originalFilePath));
			if (fileToUpload == null) throw new ArgumentNullException(nameof(fileToUpload));
			if (tags == null || tags.Count <= 0) throw new ArgumentNullException(nameof(tags));

			var container = GetStorage(organization).GetBlobContainerClient(containerName.ToLower());
			string fileName = GetFileId(newFileId, originalFilePath);
			var blob = container.GetBlobClient(fileName);

			BlobDefaultTags.SanitizeTags(tags);
			var options = new BlobUploadOptions();
			options.HttpHeaders = new BlobHttpHeaders();
			options.Tags = tags;
			options.Metadata = tags;
			options.HttpHeaders.ContentType = MimeTypeMap.GetMimeType(originalFilePath).ToLower();

			await blob.UploadAsync(fileToUpload, options);

			return fileName;
		}

		private static string GetFileId(Guid fileId, string originalFilePath)
		{
			var extension = Path.GetExtension(originalFilePath);
			return $"{fileId}{extension}".ToLower();
		}
	}
}
