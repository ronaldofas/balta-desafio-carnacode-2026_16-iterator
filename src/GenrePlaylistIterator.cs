using System.Collections.Generic;
using System.Linq;

namespace DesignPatternChallenge.IteratorDemo
{
    public class GenrePlaylistIterator : IIterator<Song>
    {
        private readonly List<Song> _filteredSongs;
        private int _position = 0;

        public GenrePlaylistIterator(List<Song> songs, string genre)
        {
            _filteredSongs = songs.Where(s => s.Genre == genre).ToList();
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
