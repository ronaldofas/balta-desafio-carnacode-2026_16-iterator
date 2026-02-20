using System.Collections.Generic;

namespace DesignPatternChallenge.IteratorDemo
{
    public class SequentialPlaylistIterator : IIterator<Song>
    {
        private readonly List<Song> _songs;
        private int _position = 0;

        public SequentialPlaylistIterator(List<Song> songs)
        {
            _songs = songs;
        }

        public bool HasNext()
        {
            return _position < _songs.Count;
        }

        public Song Next()
        {
            var song = _songs[_position];
            _position++;
            return song;
        }

        public void Reset()
        {
            _position = 0;
        }
    }
}
