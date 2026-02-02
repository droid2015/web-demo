# Hướng Dẫn Sử Dụng Presentation

Thư mục này chứa các tài liệu trình bày (presentation slides) về dự án Web Demo Platform.

## Các File Presentation

### 1. PRESENTATION.md
**Format:** Markdown  
**Mô tả:** File trình bày dạng văn bản với 21 slides chi tiết về:
- Tổng quan dự án
- Kiến trúc hệ thống
- Tính năng Backend và Frontend
- Database schema
- Module system
- Security & Authentication
- API documentation
- Deployment
- Development workflow
- Code quality
- Roadmap

**Cách sử dụng:**
- Xem trực tiếp trên GitHub
- Convert sang PDF bằng tools như [Marp](https://marp.app/), [Slidev](https://sli.dev/), hoặc Pandoc
- Sử dụng với presentation tools hỗ trợ Markdown

**Convert sang PDF với Marp:**
```bash
# Cài đặt Marp CLI
npm install -g @marp-team/marp-cli

# Convert sang PDF
marp PRESENTATION.md --pdf -o web-demo-presentation.pdf

# Convert sang PowerPoint
marp PRESENTATION.md --pptx -o web-demo-presentation.pptx
```

### 2. presentation.html
**Format:** HTML + CSS + JavaScript  
**Mô tả:** Slide deck interactive có thể mở trực tiếp trong browser với 17 slides.

**Tính năng:**
- ✨ Animation effects khi chuyển slide
- 🎨 Responsive design, đẹp mắt
- ⌨️ Điều khiển bằng keyboard:
  - `→` hoặc `Space`: Next slide
  - `←`: Previous slide
- 🖱️ Điều khiển bằng nút bấm
- 📊 Hiển thị số thứ tự slide

**Cách sử dụng:**
1. Mở file `presentation.html` trong browser:
   ```bash
   # Trên Linux/Mac
   open presentation.html
   
   # Trên Windows
   start presentation.html
   
   # Hoặc kéo thả file vào browser
   ```

2. Điều hướng:
   - Click nút "Next" / "Previous"
   - Hoặc dùng phím mũi tên trái/phải
   - Hoặc nhấn Space để next

3. Presentation mode (F11 trong hầu hết browsers để fullscreen)

## Nội Dung Trình Bày

### Phần 1: Giới Thiệu (Slides 1-3)
- Title và overview
- Tổng quan dự án
- Kiến trúc hệ thống

### Phần 2: Tính Năng (Slides 4-6)
- Backend features
- Frontend features  
- Database schema

### Phần 3: Kỹ Thuật (Slides 7-9)
- Module system
- Security & Authentication
- API endpoints

### Phần 4: Thực Hành (Slides 10-12)
- Quick start guide
- Deployment options
- Documentation

### Phần 5: Mở Rộng (Slides 13-15)
- Extension points
- Code quality
- Demo walkthrough

### Phần 6: Kết Luận (Slides 16-17)
- Key takeaways
- Thank you & Q&A

## Tips Cho Người Trình Bày

### Chuẩn Bị Trước
1. **Clone repository** và chạy thử ứng dụng
2. **Chuẩn bị demo** với data mẫu
3. **Test endpoints** trong Swagger UI
4. **Review code** quan trọng để giải thích

### Trong Buổi Trình Bày
1. **Slide 1-2:** Giới thiệu ngắn gọn (2-3 phút)
2. **Slide 3-6:** Kiến trúc và tính năng (5-7 phút)
3. **Slide 7-9:** Chi tiết kỹ thuật (5 phút)
4. **Slide 10-12:** Quick start và deployment (3 phút)
5. **Slide 13-14:** Code quality và mở rộng (3 phút)
6. **Slide 15:** **DEMO TRỰC TIẾP** (10 phút)
   - Login vào hệ thống
   - Thao tác CRUD users
   - Thao tác CRUD products
   - Show module management
   - Show Swagger UI
7. **Slide 16-17:** Tổng kết và Q&A (5 phút)

**Tổng thời gian:** ~30-35 phút

### Demo Checklist
Trước buổi trình bày, đảm bảo:
- [ ] Backend đang chạy: `http://localhost:5000`
- [ ] Frontend đang chạy: `http://localhost:5173`
- [ ] Database có seed data
- [ ] Browser đã login sẵn với admin account
- [ ] Swagger UI đã mở sẵn ở tab khác
- [ ] Có data mẫu để demo CRUD
- [ ] Network connection ổn định

### Câu Hỏi Thường Gặp

**Q: Làm sao để tạo module mới?**  
A: Follow hướng dẫn chi tiết trong DEVELOPMENT_GUIDE.md

**Q: Có support database khác ngoài Oracle không?**  
A: Hiện tại chỉ Oracle, nhưng có thể mở rộng sang SQL Server, PostgreSQL

**Q: Làm sao để deploy lên production?**  
A: Dùng Docker Compose hoặc deploy manual, xem chi tiết trong README.md

**Q: Module system hoạt động như thế nào?**  
A: Modules implement IModule interface và được auto-discover khi app start

**Q: Có unit tests không?**  
A: Chưa có, đang trong roadmap Phase 1

## Công Cụ Hỗ Trợ

### Convert Markdown sang Slides

**Marp (Recommended)**
```bash
npm install -g @marp-team/marp-cli
marp PRESENTATION.md --pdf
```

**Slidev**
```bash
npm install -g @slidev/cli
slidev PRESENTATION.md
```

**Pandoc**
```bash
pandoc PRESENTATION.md -o presentation.pptx
```

### Presentation Tools

- **reveal.js** - HTML presentation framework
- **Spectacle** - React-based presentations
- **Impress.js** - CSS3 transforms and transitions
- **Remark** - Markdown-driven slideshow

## Contributing

Nếu bạn muốn cải thiện presentation:
1. Edit file `PRESENTATION.md` hoặc `presentation.html`
2. Test kỹ trước khi commit
3. Update README này nếu cần
4. Submit pull request

## License

Cùng license với repository chính (MIT)

---

**Prepared by:** Web Demo Platform Team  
**Last Updated:** 2026-02-02  
**Version:** 1.0

🚀 **Ready to present? Let's go!**
