using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternChallenge.IteratorDemo
{
    public class ShufflePlaylistIterator : IIterator<Song>
    {
        private readonly List<Song> _shuffledSongs;
        private int _position = 0;

        public ShufflePlaylistIterator(List<Song> songs)
        {
            var random = new Random();
            _shuffledSongs = songs.OrderBy(x => random.Next()).ToList();
        }

        public bool HasNext()
        {
            return _position < _shuffledSongs.Count;
        }

        public Song Next()
        {
            var song = _shuffledSongs[_position];
            _position++;
            return song;
        }

        public void Reset()
        {
            // Opcional: Re-embaralhar no reset ou manter o mesmo embaralhamento original
            var random = new Random();
            var currentlyUnplayed = _shuffledSongs.ToList(); // Cópia para re-embaralhar
            var newlyShuffled = currentlyUnplayed.OrderBy(x => random.Next()).ToList();
            
            _shuffledSongs.Clear();
            _shuffledSongs.AddRange(newlyShuffled);
            
            _position = 0;
        }
    }
}
