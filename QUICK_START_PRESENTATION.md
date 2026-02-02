# Hướng Dẫn Nhanh - Trình Bày Slides

## 🚀 Cách Nhanh Nhất Để Trình Bày

### Bước 1: Mở Slide HTML
```bash
# Chỉ cần mở file này trong browser:
presentation.html
```

**Cách mở:**
- Windows: Double-click file `presentation.html`
- Mac: Double-click hoặc `open presentation.html`
- Linux: `xdg-open presentation.html` hoặc double-click

### Bước 2: Điều Khiển
- **Phím mũi tên phải (→)** hoặc **Space**: Slide tiếp theo
- **Phím mũi tên trái (←)**: Slide trước
- **F11**: Fullscreen mode
- **Click nút**: Dùng mouse click "Next" / "Previous"

### Bước 3: Trình Bày
- Tổng 17 slides
- Thời gian: ~30-35 phút
- Nhớ demo live ở slide 15!

---

## 📄 Nếu Cần File PDF/PowerPoint

### Cài Marp (Chỉ 1 lần)
```bash
npm install -g @marp-team/marp-cli
```

### Tạo PDF
```bash
marp PRESENTATION.md --pdf -o web-demo-slides.pdf
```

### Tạo PowerPoint
```bash
marp PRESENTATION.md --pptx -o web-demo-slides.pptx
```

---

## 📋 Checklist Trước Khi Trình Bày

### Chuẩn Bị Demo (QUAN TRỌNG!)
- [ ] Backend đang chạy: `http://localhost:5000`
- [ ] Frontend đang chạy: `http://localhost:5173`
- [ ] Database có dữ liệu mẫu
- [ ] Đã login với admin/Admin@123
- [ ] Mở sẵn Swagger UI ở tab khác
- [ ] Test CRUD operations trước

### Chuẩn Bị Slides
- [ ] Đã mở `presentation.html` trong browser
- [ ] Test phím điều khiển (mũi tên)
- [ ] Nếu cần, set fullscreen (F11)
- [ ] Bookmark các tài liệu quan trọng

---

## 🎤 Flow Trình Bày Gợi Ý

**0-2 phút:** Slides 1-2  
→ Giới thiệu project, tech stack

**2-9 phút:** Slides 3-6  
→ Kiến trúc và tính năng

**9-14 phút:** Slides 7-9  
→ Chi tiết kỹ thuật (Module, Security, API)

**14-17 phút:** Slides 10-12  
→ Quick start và deployment

**17-20 phút:** Slides 13-14  
→ Mở rộng và best practices

**20-30 phút:** Slide 15  
→ 🔴 **DEMO TRỰC TIẾP** (quan trọng nhất!)
   1. Login vào hệ thống
   2. Quản lý users (CRUD)
   3. Quản lý products (CRUD)
   4. Module management
   5. Show Swagger UI

**30-35 phút:** Slides 16-17  
→ Tổng kết và Q&A

---

## ❓ Các Câu Hỏi Thường Gặp

**Q: Làm sao tạo module mới?**  
→ Xem file `DEVELOPMENT_GUIDE.md` - có hướng dẫn chi tiết

**Q: Có support database khác không?**  
→ Hiện tại Oracle, có thể extend sang SQL Server/PostgreSQL

**Q: Deploy production như thế nào?**  
→ Dùng Docker Compose (recommended) hoặc manual

**Q: Module system hoạt động ra sao?**  
→ Auto-discovery, không cần edit Program.cs

**Q: Có unit tests chưa?**  
→ Chưa, đang trong roadmap Phase 1

---

## 💡 Tips Quan Trọng

### Trước Buổi Trình Bày
✓ Đọc lại `DEVELOPMENT_GUIDE.md`  
✓ Test toàn bộ demo flow  
✓ Chuẩn bị backup screenshots nếu demo fail  
✓ Kiểm tra network connection  

### Trong Buổi Trình Bày
✓ Nói chậm, rõ ràng  
✓ Focus vào business value, không chỉ tech  
✓ Giải thích thuật ngữ kỹ thuật  
✓ Tương tác với audience  
✓ Demo quan trọng hơn slides!  

### Nếu Demo Bị Lỗi
✓ Có backup screenshots  
✓ Giải thích bằng lời thay vì show  
✓ Hứa demo sau buổi trình bày  
✓ Đừng panic, cứ bình tĩnh tiếp tục  

---

## 📞 Cần Trợ Giúp?

**Tài liệu:**
- `PRESENTATION_README.md` - Hướng dẫn đầy đủ
- `PRESENTATION_SUMMARY.txt` - Quick reference
- `DEVELOPMENT_GUIDE.md` - Chi tiết kỹ thuật
- `README.md` - Overview project

**Repository:**  
https://github.com/droid2015/web-demo

---

## ✅ Tóm Tắt Siêu Ngắn

1. **Mở:** `presentation.html` trong browser
2. **Điều khiển:** Phím mũi tên hoặc click nút
3. **Demo:** Nhớ chuẩn bị backend + frontend running
4. **Thời gian:** ~35 phút với demo
5. **Fullscreen:** Nhấn F11

**Chúc bạn trình bày thành công! 🎉**
