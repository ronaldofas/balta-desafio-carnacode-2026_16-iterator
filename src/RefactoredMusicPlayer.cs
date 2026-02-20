using System;

namespace DesignPatternChallenge.IteratorDemo
{
    // O Cliente agora não faz ideia de como as coleções são armazenadas 
    // ou filtradas. Ele apenas recebe um "Iterador" e toca a música.
    public class RefactoredMusicPlayer
    {
        public void Play(string contextName, IIterator<Song> iterator)
        {
            Console.WriteLine($"\n=== Tocando {contextName} ===");
            
            int count = 1;
            while (iterator.HasNext())
            {
                var song = iterator.Next();
                Console.WriteLine($"{count}. {song}");
                count++;
            }
        }
    }
}
