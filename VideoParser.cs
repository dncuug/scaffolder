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

        private static string GetVideoCode(string url, out Player player)
        {
            var youtubeMatch = _youtubeVideoRegex.Match(url);
            var vimeoMatch = _vimeoVideoRegex.Match(url);

            var result = string.Empty;
            player = Player.Unknown;

            if (youtubeMatch.Success)
            {
                result = youtubeMatch.Groups[1].Value;
                player = Player.Youtube;
            }

            if (vimeoMatch.Success)
            {
                result = vimeoMatch.Groups[1].Value;
                player = Player.Vimeo;
            }

            return result;
        }

        public string GetPlayer(string url, int width = 0, int height = 315)
        {
            var strWidth = width == 0 ? "100%" : width.ToString();
            Player player;
            var videoCode = GetVideoCode(url, out player);

            switch (player)
            {
                case Player.Unknown: return url;
                case Player.Youtube: return string.Format("<iframe width=\"{0}\" height=\"{1}\" src=\"//www.youtube.com/embed/{2}\" frameborder=\"0\" allowfullscreen></iframe>", strWidth, height, videoCode);
                case Player.Vimeo: return string.Format("<iframe src=\"//player.vimeo.com/video/{2}\" width=\"{0}\" height=\"{1}\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>", strWidth, height, videoCode);
                default: return url;
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
