# DES---Digital-Signature---RSA

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 001](https://user-images.githubusercontent.com/58303745/158011634-43b27369-8934-4b25-9d26-b0cc0e2e8548.png)

**Bilgi Güvenliği ve Kriptoloji** 

Öğrenci : ***Sahil Rzayev 399973***

**RSA** 

**1.Anahtar Üretimi** 

- **p** ve **q**  asal sayıları kullanıcı tarafından ekrana giriliyor.  
- Buna göre de program tüm uygun **e** değerlerinin bir listesini kullanıcıya sunuyor ve kullanıcı seçtiği **e** değerine göre **d** değeri de otomatik program tarafından hesaplanıyor.  
- Tüm bunlar sonucunda ekrana **Genel** ve **Özel** **anahtar** değerleri basılıyor. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 002](https://user-images.githubusercontent.com/58303745/158011635-d8bd2dd8-ff8e-45fa-8cb8-4dc18c6045b0.jpeg)

**2.Şifreleme** 

- **Genel anahtarla(n,e)** şifreleme yapıldı(gönderici genel anahtarla mesajı şifreliyor). 
- ASCII değerlerinin basamak sayıları **L\_clear** ve **L\_cipher**’e (şifreli metnindeki ascii değerlerinin basamak sayısı) göre ayarlandı. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 003](https://user-images.githubusercontent.com/58303745/158011618-c6650548-0a8a-45c1-8e10-a363fa45ac23.jpeg)

**3.Deşifreleme** 

- **Özel anahtarla(d)** deşifreleme yapıldı(alıcı özel anahtarla mesajı deşifreliyor). 
- ASCII değerlerinin basamak sayıları **L\_clear** ve **L\_cipher**’e göre ayarlandı 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 004](https://user-images.githubusercontent.com/58303745/158011619-b6dd46b9-d6e8-41b1-b2d8-6bd392b9a016.jpeg)

**4.Mesaj Doğrulaması** 

- Göndericinin gönderdiği mesajla alıcının elde ettiği mesaj aynıdırsa yeşil rengli “Mesaj doğrulandı” yazısı ekrana basılıcak. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 005](https://user-images.githubusercontent.com/58303745/158011620-67648f01-d8fd-4e01-8756-8f67cf9442b5.png)

**DES** 

**1.Şifreleme** 

- Kullanıcı metni giriyor. Girilen metin ilk olarak **başlangıç permütasyon**(**IP**) uygulanıyor. Sonra 32 bitlik iki alt bloğa bölünüyor.  
- Daha sonra 16 döngü uygulanıyor. Her döngüde anahtar bitlerinden 56’dan 48’i seçiliyor.Bloğun sağ yarısı **genişletme permütasyonu**(**EP**) ile 48 bite(32 bitden 48 bite genişletme) genişletip, **XOR** işlemi uygulanarak anahtarla birleştiriliyoruz. 
- Elde edilen sonuç **S-box**’lara gönderilerek 32 yeni bit üretiliyor ve yeniden tekrar **permütasyon**(**D-box**) uygulanıyor. Elde edilen sonuç bloğun sol yarısı ile birleştirilerek **XOR** yapılır ve **sağ blo**k olarak atanır.  
- **Sol blok** sağ bloğun döngüye başlanğıcındaki halidir. 
- 16 döngüden sonra bir sonraki 64 bitlik bloğa geçilir.En sonda başlangıçta uygulanan permütasyonun tersi(**IP1**) uygulanır. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 006](https://user-images.githubusercontent.com/58303745/158011621-869b473e-ea53-4189-91c9-f1ef3944004d.png)

**2.Deşifreleme** 

- Şifrelemeden alınan şifrelenmiş metin çözülmesi için deşifrelemeye dahil ediliyor. Burada da şifrelemdeki adımların aynısı uygulanıyor. Tek fark 16 dögüden uygulanan anahtar sırası tersden uygulanıyor. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 007](https://user-images.githubusercontent.com/58303745/158011625-423a5958-be74-4e6d-a309-42a3906a6b5b.png)

**DSA (Sayısal İmza)** 

DSA Asimmetrik şifreleme için RSA algoritması kullanıldı. **DSA**’nın **RSA**’dan farkı: 

- Kullanıcı **RSA**’da girdiği metnin ASCII değerlerine göre **RSA** hesaplanırken, **DSA**’da kullanıcı’nın girdiği metnin hash’lenmiş değerlerinin ASCII değerlerine göre **DSA** hesaplanıyor. 
- Diğer bir farkı **RSA**’da **genel anahtarla** *şifreleme* yapıp, **özel anahtarla** *deşifreleme* yapıyoruz, ama  **DSA**’da **özel anahtarla** *şifreleme* yapıp, **genel anahtarla** *deşifreliyoruz*. 

**1.Anahtar Üretimi** 

- **p** ve **q**  asal sayıları kullanıcı tarafından ekrana giriliyor.  
- **d** ve **e** değerleri otomatik program tarafından hesaplanıyor.  
- Tüm bunlar sonucunda ekrana **Genel(n,e)** ve **Özel(d)** **anahtar** değerleri basılıyor. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 008](https://user-images.githubusercontent.com/58303745/158011628-eafb08aa-bc94-4897-85f0-7ac3d73af37b.png)

**2.Şifreleme** 

- **Özel anahtarla(d)** şifreleme yapıldı(gönderici özel anahtarla mesajı şifreliyor). 
- ASCII değerlerinin basamak sayıları **L\_clear** ve **L\_cipher**’e (şifreli metnindeki ascii değerlerinin basamak sayısı) göre ayarlandı. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 009](https://user-images.githubusercontent.com/58303745/158011629-861cde53-c6a6-4a00-9a57-540b5a1ca01a.jpeg)

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 010](https://user-images.githubusercontent.com/58303745/158011630-572bf99a-0b14-4dd8-aa93-43d767ed4ce9.jpeg)

**3.Deşifreleme** 

- **Genel anahtarla(n,e)** deşifreleme yapıldı(alıcı genel anahtarla mesajı deşifreliyor). 
- ASCII değerlerinin basamak sayıları **L\_clear** ve **L\_cipher**’e göre ayarlandı. 

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 011](https://user-images.githubusercontent.com/58303745/158011631-2a8256d3-fd34-45a5-9def-a508630a9d44.jpeg)

![Aspose Words e5d824c8-a23a-4168-a7b7-ffb42e0db72b 012](https://user-images.githubusercontent.com/58303745/158011632-b68492b2-3cc0-4998-9d2f-506dda709c38.jpeg)
