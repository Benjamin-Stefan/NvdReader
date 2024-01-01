namespace NvdReader.Application.Interfaces;

public interface INvdInteractor
{
    // ReSharper disable once UnusedMemberInSuper.Global
    Task HandelAsync(CancellationToken cancellationToken);
}