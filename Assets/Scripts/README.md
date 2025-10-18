# Proje İyileştirme ve Dokümantasyon

Bu doküman, projede yapılan önemli iyileştirmeleri ve eklenen yeni sistemleri açıklamaktadır.

## 1. EnemyManager (Düşman Yönetim Sistemi)

**Sorun:** Oyun, her karede (`Update` içinde) aktif düşmanları bulmak için `FindObjectsOfType<Enemy>()` fonksiyonunu kullanıyordu. Bu, özellikle sahnede çok sayıda düşman olduğunda ciddi performans sorunlarına (düşük FPS, takılmalar) yol açan çok maliyetli bir işlemdir.

**Çözüm:** Bu sorunu çözmek için merkezi bir `EnemyManager` sistemi geliştirildi.

### Nasıl Çalışır?

1.  **Merkezi Liste:** `EnemyManager`, oyundaki tüm aktif düşmanların bir listesini (`ActiveEnemies`) tutar.
2.  **Otomatik Kayıt:** Her `Enemy` betiği, `Start()` fonksiyonu çalıştığında kendini otomatik olarak `EnemyManager`'a kaydeder.
3.  **Otomatik Kaldırma:** Düşman yok edildiğinde (`OnDestroy()` fonksiyonu çalıştığında), kendini otomatik olarak `EnemyManager` listesinden kaldırır.
4.  **Verimli Erişim:** `WeaponController` gibi diğer betikler, en yakın düşmanı bulmak için artık tüm sahneyi taramak yerine doğrudan `EnemyManager.Instance.ActiveEnemies` listesine erişir. Bu, performansı önemli ölçüde artırır.

### Kurulum

`EnemyManager` sisteminin çalışması için, oyun sahnenizde (örneğin "Managers" adında boş bir GameObject oluşturup) `EnemyManager.cs` betiğini bu GameObject'e eklemeniz **gerekmektedir**. Aksi takdirde sistem çalışmaz ve düşmanlar hedef alınamaz.

Bu iyileştirme sayesinde oyun, düşman sayısı artsa bile daha stabil ve performanslı çalışacaktır.