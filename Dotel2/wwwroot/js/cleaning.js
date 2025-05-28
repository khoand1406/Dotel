showCleaningService();
showCDetails();

function showCleaningService() {
    const url1 = "../json/cleaningservicedetail.json";
fetch(url1)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {
        var html = posts.map(function(post) {
            let star  = post.star;
            return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.1s">
            <div class="room-item shadow rounded overflow-hidden">
                <div class="position-relative zoom">
                    <img class="img-fluid" src="${post.image1}" alt="">
                    <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}
                    </small>
                </div>
                <div class="p-4 mt-2">
                    <div class="d-flex justify-content-between mb-3">
                        <h5 class="mb-0">${post.title}</h5>
                        <div id ="star" class="ps-2">` + showStar(star) + `</div>
                    </div>
                   
                    <p class="text-body mb-3">${post.shortDes}</p>
                    <div class="d-flex justify-content-between">
                        <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="CDetails(${post.id})">Xem chi tiết</a>
                        <a class="btn btn-sm btn-dark rounded py-2 px-4" rel="nofollow" href="${post.Zalo}">Liên hệ</a>
                    </div>
                </div>
            </div>
        </div>`;
  })
                 var htmls = html.join('');    
                 document.getElementById("cleaningPost").innerHTML = htmls;
})
}

function showCDetails() {
    const url3 = "../json/cleaningservicedetail.json";
    fetch(url3)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(post) {
    // lấy id từ localstorage
    var id = localStorage.getItem("ID_POST");
    // tìm post có id trùng với id lấy từ localstorage
    var post = post.find((post) => post.id == id);  
    // gán phần tử post vào mảng mới
    var detailpost = [post];

    // lấy mảng mới và map ra html
    var info = detailpost.map(function(post) {
        let star  = post.star;
        return `<div class="aa-properties-details-img">
               
        <img src="${post.image1}" alt="img">
      </div>
      <div class="aa-properties-info">
        <h2>Dọn phòng ${post.title}
        </h2>
       
        <span class="aa-price">${post.price}</span>
        <div id ="star" class="ps-2">` + showStar(star) + `</div>   
        </span>
        <p>${post.description1}</p>
        <p>${post.description2}</p>
        <p>${post.description3}</p>
      </div>

      <section class="section post-map"><div class="section-header"><h3 >Bản đồ</h3>
       <address class="section-description">Địa chỉ: ${post.address}</address></div>
       <div class="section-content">
         <div id="__maps_content" style="width:100%; height:300px;" data-lat="" data-long="" data-address="18/5 Nguyễn Hới, Phường An Lạc, Quận Bình Tân, Hồ Chí Minh">
           <iframe width="100%" height="100%" frameborder="0" style="border:0" src="img/map.jpg" allowfullscreen=""></iframe>
         </div>
       </div>
     </section>`; 
    })
    var contact = detailpost.map(function(post) {
      return `<h3>Bảng giá dịch vụ</h3>  
      <table style ="font: sans-serif" class="aqua_table">
        <tbody>
        <tr>
        <th style="color: black; font-weight:bold "> Dịch vụ vệ sinh theo giờ</th>
        <th style="color: black; font-weight:bold ">Đơn giá VND/1H</th>
        </tr>
        <tr>
        <td>Vệ sinh gấp trong ngày:</td>
        <td style="text-align: center;">${post.pricetable[0]}</td>
        </tr>
        <tr>
        <td>Báo trước 24h:</td>
        <td style="text-align: center;">${post.pricetable[1]}</td>
        </tr>
        <tr>
        <td>Định kỳ hàng tuần:</td>
        <td style="text-align: center;">${post.pricetable[2]}</td>
        </tr>
        <tr>
        <td>Bao gồm:</td>
        <td style="text-align: center;">${post.pricetable[3]}</td>
        </tr>
        </tbody>
        </table>
    <h3>Thông tin liên hệ</h3>
        <div class="aa-properties-single-sidebar">
          <h6 class = "">Dọn phòng ${post.title}</h6>
            <span>Số điện thoại: </span>
                  <a  rel="nofollow" href="tel:${post.phone}">
                    <i></i> ${post.contact[1]}</a>
                    <br>
                      <a class="btn btn-primary w-100" target="_blank" rel="nofollow"
                      href="${post.contact[2]}">Liên hệ Zalo</a>
         <br>
        </div>`

    })
    document.getElementById("info").innerHTML = info;
    document.getElementById("contact").innerHTML = contact;
   
  })
}
function CDetails(id) {
    localStorage.setItem("ID_POST", id);
    window.location.href = "CleaningServiceDetails.html";
    }

function showStar(star) {
    let html = '';
    for (let i = 1; i <= star; i++) {
        html += `<small class="fa fa-star text-primary"></small>`;
    }
    return html;
    }
