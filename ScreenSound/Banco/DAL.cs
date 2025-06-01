namespace ScreenSound.Banco;

internal class DAL<T> where T : class
{
    protected readonly ScreenSoundContext context;

    public DAL(ScreenSoundContext context)
    {
        this.context = context;
    }

    public IEnumerable<T> Listar()
    {
        return context.Set<T>().ToList();
    }

    public void Adicionar(T obj)
    {
        context.Set<T>().Add(obj);
        context.SaveChanges();
    }

    public void Atualizar(T obj, int id)
    {
        var artistaExistente = context.Set<T>().Find(id);

        if (artistaExistente == null)
        {
            throw new ArgumentException($"Artista com ID {id} não encontrado.");
        }

        context.Set<T>().Update(obj);
        context.SaveChanges();
    }

    public async Task Excluir(int id)
    {
        var artistaExistente = await context.Set<T>().FindAsync(id);

        if (artistaExistente == null)
        {
            throw new ArgumentException($"Artista com ID {id} não encontrado.");
        }

        context.Set<T>().Remove(artistaExistente);
        await context.SaveChangesAsync();
    }

    public T? RecuperarPor(Func<T, bool> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }

}
