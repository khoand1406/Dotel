
showTransportService();
showDDetails();


function showTransportService() {
    const url2 = "../json/DeliveryDetail.json";
    fetch(url2)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {

        var html = posts.map(function(post) {
            console.log(post);
            let star  = post.star;
            return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.1s">
            <div class="room-item shadow rounded overflow-hidden">
                <div class="position-relative zoom">
                    <img class="img-fluid" src="${post.image}" alt="">
                    <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
                </div>
                <div class="p-4 mt-2">
                    <div class="d-flex justify-content-between mb-0">
                        <h5 class="mb-1">${post.shortTitle}</h5>
                        <div id ="star" class="ps-2">` + showStar(star) + `</div>
                    </div>
                    <div class= "d-flex justify-content-between">
                        <small class="mb-1"><i class="fa fa-map-marker"  font-size: 15px></i> ${post.place}</small>
                    </div>

                    <p class="text-body mb-3">${post.shortDes}</p>
                    <div class="d-flex justify-content-between">
                        <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="DDetails(${post.id})">Chi tiết</a>
                        <a class="btn btn-sm btn-dark rounded py-2 px-4" href="${post.Zalo}">Liên hệ</a>
                    </div>
                </div>
            </div>
        </div>`;
  })
                 var htmls = html.join('');    
                 document.getElementById("delivery").innerHTML = htmls;
})
}
function showDDetails() {
  const url3 = "../json/DeliveryDetail.json";
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
               
    <img src="${post.image}" alt="img">

  </div>
  <div class="aa-properties-info mb-0">
  <br>
    <h2>${post.title}</h2>
    <span class="aa-location"><div class="me-2 mb-3 fa fa-map-marker" ></div>${post.place}</span>
    <div id ="star" class="ps-2">` + showStar(star) + `</div>   
    <p style="text-align:justify">${post.shortDes}</p>
         
     <h4>${post.p1[0]}</h4>
     <ul>
         <li>${post.p1[1]}</li>
          <li>${post.p1[2]}</li>
          <li>${post.p1[3]}</li>
          <li>${post.p1[4]}</li> 
     </ul>
     <h4>${post.p2[0]}</h4>
     <ul>
         <li>${post.p2[1]}</li>
          <li>${post.p2[2]}</li>
          <li>${post.p2[3]}</li>

     </ul>
     <h4>${post.p3[0]}</h4>
     <ul>
         <li>${post.p3[1]}</li>
          <li>${post.p3[2]}</li>
     </ul>
        
            
  </div>

  <section class="section post-map"><div class="section-header"><h3 >Bản đồ</h3>
   <address class="section-description"${post.address}</address></div>
   <div class="section-content">
     <div id="__maps_content" style="width:100%; height:300px;" data-lat="" data-long="" data-address="18/5 Nguyễn Hới, Phường An Lạc, Quận Bình Tân, Hồ Chí Minh">
       <iframe width="100%" height="100%" frameborder="0" style="border:0" src="img/map.jpg" allowfullscreen=""></iframe>
     </div>
   </div>
 </section>`

})
var contact = detailpost.map(function(post) {
    return ` <h3>Dịch vụ chúng tôi gồm có</h3>
    <ul>
      <li>${post.p4[1]}</li>
      <li>${post.p4[2]}</li>
      <li>${post.p4[3]}</li>
      <li>${post.p4[4]}</li>
    </ul>
    

    <h3>Bảng giá dịch vụ</h3>  
    <table>
        <tr>
           <th style="color:black">LOẠI XE</th>
           <th style="color:black">GIÁ MỞ CỬA – 10KM ĐẦU</th>
           <th style="color:black">TỪ KM THỨ 11 ĐẾN 44</th>
           <th style="color:black">TỪ KM THỨ 45</th>
           <th style="color:black">THỜI GIAN CHỜ</th>
        </tr>
        <tr>
          <td>Xe 500kg</td>
          <td>${post.priceRow1[0]}</td>
          <td>${post.priceRow1[1]}</td>
          <td>${post.priceRow1[2]}</td>
          <td>${post.priceRow1[3]}</td>
        </tr>
        <tr>
          <td>Xe 750kg</td>
          <td>${post.priceRow2[0]}</td>
          <td>${post.priceRow2[1]}</td>
          <td>${post.priceRow2[2]}</td>
          <td>${post.priceRow2[3]}</td>
        </tr>
        <tr>
            <td>Xe 1.5 tấn</td>
            <td>${post.priceRow3[0]}</td>
          <td>${post.priceRow3[1]}</td>
          <td>${post.priceRow3[2]}</td>
          <td>${post.priceRow3[3]}</td>
          </tr>
          <tr>
            <td>Xe 1.9 tấn</td>
            <td>${post.priceRow4[0]}</td>
          <td>${post.priceRow4[1]}</td>
          <td>${post.priceRow4[2]}</td>
          <td>${post.priceRow4[3]}</td>
          </tr>
          <tr>
            <td>Xe 2 tấn</td>
            <td>${post.priceRow5[0]}</td>
          <td>${post.priceRow5[1]}</td>
          <td>${post.priceRow5[2]}</td>
          <td>${post.priceRow5[3]}</td>
          </tr>   
      </table>
     <br>
    <h3>Thông tin liên hệ</h3>
        <div class="aa-properties-single-sidebar">
          <h6 class = "">${post.name}</h6>
            <span>Số điện thoại: </span>
                  <a  rel="nofollow" href="tel:${post.phone}">
                    <i></i> ${post.phone}</a>
                    <br>
                      <a class="btn btn-primary w-100" target="_blank" rel="nofollow"
                      href="${post.Zalo}">Liên hệ Zalo</a>
        <br>
        </div>`

})
document.getElementById("info").innerHTML = info;
document.getElementById("contact").innerHTML = contact;

})
}

function DDetails(id) {
    localStorage.setItem("ID_POST", id);
    window.location.href = "DeliveryDetails.html";
    }

function showStar(star) {
    let html = '';
    for (let i = 1; i <= star; i++) {
        html += `<small class="fa fa-star text-primary"></small>`;
    }
    return html;
    }

function showSpecific(){

}


