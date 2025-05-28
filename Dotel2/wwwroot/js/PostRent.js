getHotNews();
getNormalNews();
ShowPostDetails();

function getHotNews() {
    const url1 = "../json/PostRent.json";
fetch(url1)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {

    // lọc ra 3 post có id nhỏ nhất
    var hotposts = posts.filter((post) => post.id < 4);
        var html = hotposts.map(function(post) {
            let star  = post.star;
            return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.1s">
            <div class="room-item shadow rounded overflow-hidden">
                <div class="position-relative zoom">
                    <img class="img-fluid" src="${post.image1}" alt="">
                    <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
                </div>
                <div class="p-4 mt-2">
                    <div class="d-flex justify-content-between mb-3">
                        <h5 class="mb-0">${post.title}</h5>
                        <div id ="star" class="ps-2">` + showStar(star) + `</div>
                    </div>
                    <div class="d-flex mb-3">
                        <small class="border-end me-3 pe-3"><i class="fa fa-bed text-primary me-2"></i>${post.bed} Phòng ngủ</small>
                        <small class="border-end me-3 pe-3"><i class="fa fa-bath text-primary me-2"></i>${post.bath} Phòng tắm</small>
                        <small><i class="fa fa-wifi text-primary me-2"></i>Wifi</small>
                    </div>
                    <p class="text-body mb-3">${post.shortDes}</p>
                    <div class="d-flex justify-content-between">
                    <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="Details(${post.id})"> Chi Tiết </a>
                        <div>
                            <a class="btn btn-outline-light btn-social"target="_blank" rel="nofollow" href="${post.contact[2]}"><img src="img/facebook-seeklogo.com.svg" width="25px" height="25px"></img></a>
                            <a class="btn btn-outline-light btn-social"target="_blank" rel="nofollow" href="${post.contact[3]}"><img src="img/zalo-seeklogo.com.svg" width="25px" height="25px"></img></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
  })
                 var htmls = html.join('');    
                 document.getElementById("HotNews").innerHTML = htmls;
})
}

function getNormalNews() {
    const url2 = "../json/PostRent.json";
    fetch(url2)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {

    // lọc ra post có id lớn hơn 3 
    var normalposts = posts.filter((post) => post.id > 3);
        var html = normalposts.map(function(post) {
            console.log(post);
            let star  = post.star;
            return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.1s">
            <div class="room-item shadow rounded overflow-hidden">
                <div class="position-relative zoom">
                    <img class="img-fluid" src="${post.image1}" alt="">
                    <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
                </div>
                <div class="p-4 mt-2">
                    <div class="d-flex justify-content-between mb-3">
                        <h5 class="mb-0">${post.title}</h5>
                        <div id ="star" class="ps-2">` + showStar(star) + `</div>
                    </div>
                    <div class="d-flex mb-3">
                        <small class="border-end me-3 pe-3"><i class="fa fa-bed text-primary me-2"></i>${post.bed} Phòng ngủ</small>
                        <small class="border-end me-3 pe-3"><i class="fa fa-bath text-primary me-2"></i>${post.bath} Phòng tắm</small>
                        <small><i class="fa fa-wifi text-primary me-2"></i>Wifi</small>
                    </div>
                    <p class="text-body mb-3">${post.shortDes}</p>
                    <div class="d-flex justify-content-between">
                    <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="Details(${post.id})"> Chi Tiết </a>
                        <div>
                            <a class="btn btn-outline-light btn-social"target="_blank" rel="nofollow" href="${post.contact[2]}"><img src="img/facebook-seeklogo.com.svg" width="25px" height="25px"></img></a>
                            <a class="btn btn-outline-light btn-social"target="_blank" rel="nofollow" href="${post.contact[3]}"><img src="img/zalo-seeklogo.com.svg" width="25px" height="25px"></img></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
  })
                 var htmls = html.join('');    
                 document.getElementById("NormalNews").innerHTML = htmls;
})
}
function ShowPostDetails() {
    const url3 = "../json/PostRent.json";
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
        return ` <div class="aa-properties-content">            
          <div class="aa-properties-details">
          <div class="aa-properties-details-img">
            
            <div class="aa-properties-details-img">
                <div id="header-carousel" class="carousel slide" data-bs-ride="carousel">
                  <div class="carousel-inner">
                      <div class="carousel-item active">
                          <img class="img-fluid" src="${post.image1}" alt="Image">
                      </div>
                      <div class="carousel-item">
                          <img class="w-100" src="${post.image2}" alt="Image">
                      </div>
                      <div class="carousel-item">
                          <img class="w-100" src="${post.image3}" alt="Image">
                      </div>
                  </div>

                    <button class="carousel-control-prev" type="button" data-bs-target="#header-carousel"
                        data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#header-carousel"
                        data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
              </div>
            </div>
  
          <div class="aa-properties-info mb-0">
            <h1 style="color: #FEA116; margin-top: 15px;">${post.title}</h1>           
            <div id ="star" class="ps-2">` + showStar(star) + `</div>        
            <h5><i class="fa-solid fa-money-bill", style="color: #FEA116;"></i> &nbsp; ${post.price}</h5>
            <h5><i class="fa-solid fa-location-dot", style="color: #FEA116;"></i> &nbsp; ${post.address}</h5>
            <p style="text-align: justify;">${post.description[0]}</p>
            <p style="text-align: justify;">${post.description[1]}</p>
            <p style="text-align: justify;">${post.description[2]}</p>  
            <p style="text-align: justify;">${post.description[3]}</p>
          </div>

          <section class="section post-map"><div class="section-header"><h3 >Bản đồ</h3>
            <address class="section-description">${post.address}</address></div>
            <div class="section-content">
              <div id="__maps_content" style="width:100%; height:300px;" data-lat="" data-long="" data-address="18/5 Nguyễn Hới, Phường An Lạc, Quận Bình Tân, Hồ Chí Minh">
                <iframe width="100%" height="100%" frameborder="0" style="border:0" src="img/map.jpg" allowfullscreen=""></iframe>
              </div>
            </div>
          </section>

          </div>           
        </div>
        </div>`; 
    })
    var contact = detailpost.map(function(post) {
      return `<div class="aa-properties-info">
      <h4>Cơ sở vật chất</h4>
    <ul>
      <li><i class="fa-solid fa-bed"></i> &nbsp;${post.facilities[0]}</li>
      <li><i class="fa-solid fa-bath"></i> &nbsp;${post.facilities[1]}</li>
      <li><i class="fa-solid fa-utensils"></i> &nbsp;${post.facilities[2]}</li>
      <li><i class="fa-solid fa-temperature-arrow-up"></i> &nbsp;${post.facilities[3]}</li>
      <li><i class="fa-solid fa-door-open"></i> &nbsp;${post.facilities[4]}</li>
      <li><i class="fa-solid fa-tree"></i> &nbsp;${post.facilities[5]}</li>
      <li><i class="fa-solid fa-shield-halved"></i> &nbsp;${post.facilities[6]}</li>
   
    </ul>
    </div>
    <h4>Thông tin liên hệ</h4>
        <div class="aa-properties-single-sidebar">                                  
            <h5 class = "">${post.contact[0]}</h5>
          <span>Số điện thoại: </span>
                <a  rel="nofollow" href="tel:${post.contact[1]}">
                  <i></i> ${post.contact[1]}</a>
                  <br>
            <span>Facebook: </span>
            <a  rel="nofollow" target="_blank" href="${post.contact[2]}">
              <i></i> ${post.contact[0]}</a>
              <br>
            
            <span>${post.address}</span>                  
                  
                    <a class="btn btn-primary w-100" target="_blank" rel="nofollow"
                    href="${post.contact[3]}">Liên hệ Zalo</a>
                    <br>
        </div>`

    })
    document.getElementById("info").innerHTML = info;
    document.getElementById("contact").innerHTML = contact;
   
  })
}

function Details(id) {
    localStorage.setItem("ID_POST", id);
    window.location.href = "RentHomeDetails.html";
    }

function showStar(star) {
    let html = '';
    for (let i = 1; i <= star; i++) {
        html += `<small class="fa fa-star text-primary"></small>`;
    }
    return html;
    }


