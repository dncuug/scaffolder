using System.Text.RegularExpressions;

namespace X.Scaffolding
{
    internal class VideoParser
    {
        private static Regex _youtubeVideoRegex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
        private static Regex _vimeoVideoRegex = new Regex("vimeo\\.com/(?:.*#|.*/videos/)?([0-9]+)");

        static VideoParser()
        {
        }

        private string GetVideoCode(string url, out VideoParser.Player player)
        {
            Match match1 = VideoParser._youtubeVideoRegex.Match(url);
            Match match2 = VideoParser._vimeoVideoRegex.Match(url);
            string str = string.Empty;
            player = VideoParser.Player.Unknown;
            if (match1.Success)
            {
                str = match1.Groups[1].Value;
                player = VideoParser.Player.Youtube;
            }
            if (match2.Success)
            {
                str = match2.Groups[1].Value;
                player = VideoParser.Player.Vimeo;
            }
            return str;
        }

        public string GetPlayer(string url, int width = 0, int height = 315)
        {
            string str = width == 0 ? "100%" : width.ToString();
            VideoParser.Player player;
            string videoCode = this.GetVideoCode(url, out player);
            switch (player)
            {
                case VideoParser.Player.Unknown:
                    return url;
                case VideoParser.Player.Youtube:
                    return string.Format("<iframe width=\"{0}\" height=\"{1}\" src=\"//www.youtube.com/embed/{2}\" frameborder=\"0\" allowfullscreen></iframe>", (object)str, (object)height, (object)videoCode);
                case VideoParser.Player.Vimeo:
                    return string.Format("<iframe src=\"//player.vimeo.com/video/{2}\" width=\"{0}\" height=\"{1}\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>", (object)str, (object)height, (object)videoCode);
                default:
                    return url;
            }
        }

        private enum Player
        {
            Unknown,
            Youtube,
            Vimeo,
        }
    }
}
