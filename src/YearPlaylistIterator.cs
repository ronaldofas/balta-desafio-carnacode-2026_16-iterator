using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternChallenge.IteratorDemo
{
    public class YearPlaylistIterator : IIterator<Song>
    {
        private readonly List<Song> _filteredSongs;
        private int _position = 0;

        public YearPlaylistIterator(List<Song> songs, int yearThreshold, bool isOlderThan)
        {
            if (isOlderThan)
            {
                _filteredSongs = songs.Where(s => s.Year < yearThreshold).OrderBy(s => s.Year).ToList();
            }
            else
            {
                 _filteredSongs = songs.Where(s => s.Year >= yearThreshold).OrderBy(s => s.Year).ToList();
            }
        }

        public bool HasNext()
        {
            return _position < _filteredSongs.Count;
        }

        public Song Next()
        {
            var song = _filteredSongs[_position];
            _position++;
            return song;
        }

        public void Reset()
        {
            _position = 0;
        }
    }
}
