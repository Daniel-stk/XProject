﻿using AxWMPLib;
using System;
using System.Collections.Generic;
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
            return null;
        }
    }
}