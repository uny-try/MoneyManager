using System;

namespace MoneyManager.ViewModels.Interfaces;

/// <summary>
/// ページ間のナビゲーションを管理するサービスのインターフェース
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// 指定されたルートにナビゲートします
    /// </summary>
    Task NavigateToAsync(string route);

    /// <summary>
    /// 前のページに戻ります
    /// </summary>
    Task GoBackAsync();
}
