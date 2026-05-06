List<Kendaraan> datakendaraan = new List<Kendaraan>()
{
 new Kendaraan("Beat", "L 3290 N", 2300000),
 new Kendaraan("Nmax", "M 2398 N", 3900000),
 new Mobil("Porche", "N 2133 N", 400000),
 new Mobil("Tehrios", "W 3455 N", 900000),
 new Minibus("Lokobus", "J 9182 M", 20000),
 new Minibus("Jukobus", "F 2340 M", 200000)
};

while (true)
{
    Console.Clear();

    Console.WriteLine("----- Rental kendaraan UKOLA -----");
    Console.WriteLine("\nDaftar Kendaraan");
    foreach (var kendaraan in datakendaraan)
    {
        kendaraan.TampilInfo();
    }
    Console.WriteLine("1. Sewa\n2. Kembali\n3. keluar");
    Console.WriteLine("Pilihan: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {

        Console.Write("Nama kendaraan yang disewa: ");
        string kendaraan_sewa = Console.ReadLine();

        var cari_kendaraan = datakendaraan.FirstOrDefault(ck => string.Equals(kendaraan_sewa, ck.NamaKendaraan, StringComparison.OrdinalIgnoreCase));  // StringComaparison.OrdinalIgnoreCase = untuk mangabaikan perbedaan huruf (jadi huruf besar dan kecil dianggap sama)

        if (cari_kendaraan == null)
        {

            Console.WriteLine($"\n Kendaraan dengan nama {kendaraan_sewa} Tidak ditemukan, mohon masukkan yang sesuai ya");

        }
        else if (cari_kendaraan.IsAvailable) // boleh pake ( = True) tapi sama saja
        {
            Console.WriteLine("\nInput jumlah hari");
            int hari = int.Parse(Console.ReadLine());

            double total_sewa = cari_kendaraan.HitungTotal(hari);

            Console.WriteLine($"Total pembayaran sewa: Rp {total_sewa} ");

            cari_kendaraan.UbahStatus();
        }
        else
        {
            Console.WriteLine($"\n Kendaraan dengan nama {cari_kendaraan} tidak ditemukan");
        }
    }
    else if (pilihan == "2")
    {
        Console.Write("Nama kendaraan yang disewa: ");
        string kendaraan_sewa = Console.ReadLine();

        var cari_kendaraan = datakendaraan.FirstOrDefault(ck => string.Equals(kendaraan_sewa, ck.NamaKendaraan, StringComparison.OrdinalIgnoreCase));

        if (cari_kendaraan == null)
        {

            Console.WriteLine($"\n Kendaraan dengan nama {kendaraan_sewa} Tidak ditemukan, mohon masukkan yang sesuai ya");

        }
        else if (!cari_kendaraan.IsAvailable)
        {
            cari_kendaraan.UbahStatus();
        }
        else
        {
            Console.WriteLine($"\n kendaraan dengan nama {kendaraan_sewa} tersedia");
        }

    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\n tekan ENTER untuk keluar dari dan menutup aplikasi ");
        Console.ReadLine();
        break;
    }
    else
    {
        Console.WriteLine("\npilihan invalid");
    }

    Console.WriteLine("\nTekann ENTER untuk mengulang");
    Console.ReadLine();

}

class Kendaraan
{
    protected string _namaKendaraan;
    protected string _nomorPolisi;
    protected double _hargaSewa;
    protected bool _isAvailable;

    public Kendaraan(string nama_kendaraan, string nomor_polisi, double harga_sewa)
    {
        _namaKendaraan = nama_kendaraan;
        _nomorPolisi = nomor_polisi;
        _hargaSewa = harga_sewa;
        _isAvailable = true;
    }

    public string NamaKendaraan
    {
        get { return _namaKendaraan; }
        set { _namaKendaraan = value; }
    }

    public string NomorPolisi
    {
        get { return _nomorPolisi; }
    }

    public double HargaSewa
    {
        get { return _hargaSewa; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Harga sewa tidak boleh negatif!");
            }
            else
            {
                _hargaSewa = value;
            }
        }
    }

    public bool IsAvailable
    {
        get { return _isAvailable; }
    }

    public void TampilInfo()
    {
        string status = _isAvailable ? "Tersedia" : "Tidak tersedia";
        Console.WriteLine($"{_namaKendaraan} | {_nomorPolisi} | Rp {_hargaSewa} / hari | {status} ");
    }

    public void UbahStatus()
    {
        _isAvailable = !_isAvailable;
    }

    public virtual double HitungTotal(int hari)
    {
        return _hargaSewa * hari;
    }
}

class Mobil : Kendaraan
{
    private double _biayaasuransi;

    public Mobil(string nama_kendaraan, string nomor_polisi, double harga_sewa) : base(nama_kendaraan, nomor_polisi, harga_sewa)
    {
        _biayaasuransi = 50000;

    }

    public override double HitungTotal(int hari)
    {
        return base.HitungTotal(hari) + _biayaasuransi;
    }
}

class Minibus : Kendaraan
{
    private double _biayaSopir;

    public Minibus(string nama_kendaraan, string nomor_polisi, double harga_sewa) : base(nama_kendaraan, nomor_polisi, harga_sewa)
    {

        _biayaSopir = 100000;

    }

    public override double HitungTotal(int hari)
    {
        return base.HitungTotal(hari) + _biayaSopir * hari;
    }
}