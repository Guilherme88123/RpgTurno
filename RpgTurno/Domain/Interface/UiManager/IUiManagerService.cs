using Application.Model.MenuElements.Base;

namespace Domain.Interface.UiManager;

/// <summary>
/// Serviço de gerenciamento de elementos gráficos da tela, como textos, componentes (botões, etc) e animações. 
/// Ele é responsável por adicionar, atualizar, desenhar e limpar esses elementos conforme necessário 
/// durante o jogo.
/// </summary>
public interface IUiManagerService
{
    void AddComponent(BaseComponent component);
    void AddComponent(List<BaseComponent> components);

    void ClearComponents();
    void UpdateComponents();
    void DrawComponents();
}
