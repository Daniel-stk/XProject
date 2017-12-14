using AxWMPLib;
using Caroto.DomainObjects;
using Caroto.Tools;
using Gateway;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Caroto.Services
{
    public class WindowsMediaPlayerService
    {
        private static readonly Lazy<WindowsMediaPlayerService> _instance = new Lazy<WindowsMediaPlayerService>(() => new WindowsMediaPlayerService());
        private static AxWindowsMediaPlayer _windowsMediaPlayer;

        public static WindowsMediaPlayerService Instace
        {
            get
            {
                return _instance.Value;
            }
        }

        private WindowsMediaPlayerService()
        {

        }

        public void SetMediaPlayerInstance(AxWindowsMediaPlayer windowsMediaPlayer)
        {
            _windowsMediaPlayer = windowsMediaPlayer;
        }

        public IWMPPlaylist ComposePlaylist(List<string> videos)
        {
            var playlist = _windowsMediaPlayer.playlistCollection.newPlaylist("currentPlayList");
            IWMPMedia media;
            foreach(var video in videos)
            {
                media = _windowsMediaPlayer.newMedia(CarotoSettings.Default.VideoFolder+@"\videos\"+video);
                playlist.appendItem(media);
            }
            return playlist;
        }

        public Sequence GetNextSequence()
        {
            if(File.Exists(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json"))
            {
               return JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json");
            }
            return null;
        }
    }
}
