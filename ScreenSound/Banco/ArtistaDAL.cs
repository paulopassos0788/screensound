using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ArtistaDAL
{
    private readonly ScreenSoundContext context;

    public ArtistaDAL(ScreenSoundContext context)
    {
        this.context = context;
    }

    public IEnumerable<Artista> Listar()
    {
        return context.Artistas.ToList();
    }

    public void Adicionar(Artista artista)
    {
        context.Artistas.Add(artista);
        context.SaveChanges();
        var lista = new List<Artista>();
    }

    public void Atualizar(Artista artista, int id)
    {
        var artistaExistente = context.Artistas.Find(id);

        if (artistaExistente == null)
        {
            throw new ArgumentException($"Artista com ID {id} não encontrado.");
        }

        artistaExistente.Nome = artista.Nome;
        artistaExistente.Bio = artista.Bio;

        context.SaveChanges();
    }

    public async Task Excluir(int id)
    {
        var artistaExistente = await context.Artistas.FindAsync(id);

        if (artistaExistente == null)
        {
            throw new ArgumentException($"Artista com ID {id} não encontrado.");
        }

        context.Artistas.Remove(artistaExistente);
        await context.SaveChangesAsync();
    }

    public Artista? PesquisarArtistaPorNome(string nome)
    {
        return context.Artistas.FirstOrDefault(a => a.Nome.Equals(nome));
    }
}