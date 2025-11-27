import { Injectable } from '@angular/core';
import { AuthenticationService } from './auth.service';
import { MenuItem } from '../../menu/menu.model';

@Injectable({
  providedIn: 'root',
})
export class MenuAuthorizationService {
  private userRoles: string[] = [];

  constructor(private authService: AuthenticationService) {
    // Constructor'da loadUserRoles çağırmıyoruz
    // Her filtreleme işleminde güncel rolleri alacağız
  }

  /**
   * Kullanıcı rollerini yükle
   */
  private loadUserRoles(): void {
    const currentUser = this.authService.currentUser();
    if (currentUser && currentUser.role) {
      // Rolleri virgülle ayrılmış string'den array'e çevir
      if (typeof currentUser.role === 'string') {
        this.userRoles = currentUser.role.split(',').map((role) => role.trim());
      } else if (Array.isArray(currentUser.role)) {
        this.userRoles = currentUser.role;
      } else {
        this.userRoles = [];
      }
    } else {
      this.userRoles = [];
    }
  }

  /**
   * Kullanıcının role göre menüyü filtrele
   */
  filterMenuByRole(menuItems: MenuItem[]): MenuItem[] {
    // Her filtreleme işleminde güncel rolleri yükle
    this.loadUserRoles();

    return menuItems
      .filter((item) => this.hasRoleAccess(item))
      .map((item) => {
        if (item.subItems && item.subItems.length > 0) {
          return {
            ...item,
            subItems: this.filterMenuByRole(item.subItems),
          };
        }
        return item;
      })
      .filter((item) => {
        // Eğer bir menünün alt menüleri varsa ve hiçbiri görünmüyorsa, üst menüyü de gösterme
        if (item.subItems && item.subItems.length === 0 && !item.link) {
          return false;
        }
        return true;
      });
  }

  /**
   * Kullanıcının belirli bir menüye erişim yetkisi var mı kontrol et
   */
  hasRoleAccess(item: MenuItem): boolean {
    // Eğer menüde allowedRoles tanımlı değilse, herkes görebilir
    if (!item.allowedRoles || item.allowedRoles.length === 0) {
      return true;
    }

    // Kullanıcının rollerinden herhangi biri allowedRoles içinde var mı?
    return this.userRoles.some((userRole) =>
      item.allowedRoles!.includes(userRole)
    );
  }

  /**
   * Kullanıcı rollerini al (güncel rolleri yükleyerek)
   */
  getUserRoles(): string[] {
    this.loadUserRoles();
    return this.userRoles;
  }

  /**
   * Kullanıcı rollerini güncelle (login/logout durumlarında)
   * @deprecated Bu metod artık gerekli değil, filterMenuByRole otomatik olarak güncelliyor
   */
  refreshUserRoles(): void {
    this.loadUserRoles();
  }
}
