getHotNews1();
getNormalNews1();
getHotNews2();
getNormalNews2();
ShowPostDetails();

function getHotNews1() {
    const url1 = "../json/Roommate.json";
fetch(url1)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {

    var hotposts = posts.filter((post) => post.id < 4);
        var html = hotposts.map(function(post) {          
            return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.6s">
            <div class="room-item shadow rounded overflow-hidden">
                <div class="position-relative zoom">
                    <img class="img-fluid" src="${post.image1}" alt="">
                    <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
                </div>
                <div class="p-4 mt-2">
                    <div class="d-flex justify-content-between mb-3">
                        <h5 class="mb-0">${post.title}</h5>
                        <div class="ps-2">
                            <h6 class="mb-0">${post.genderAge}</h6>
                        </div>
                    </div>
                    <div class="d-flex mb-3">
                        <small class="border-end me-3 pe-3"><i class="fa fa-bed text-primary me-2"></i>${post.bed} Giường</small>
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
                 document.getElementById("HotNews1").innerHTML = htmls;
})
}

function getNormalNews1() {
    const url2 = "../json/Roommate.json";
    fetch(url2)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {

    var normalposts = posts.filter((post) => post.id > 3 && post.id < 7);
    var html = normalposts.map(function(post) {          
      return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.6s">
      <div class="room-item shadow rounded overflow-hidden">
          <div class="position-relative zoom">
              <img class="img-fluid" src="${post.image1}" alt="">
              <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
          </div>
          <div class="p-4 mt-2">
              <div class="d-flex justify-content-between mb-3">
                  <h5 class="mb-0">${post.title}</h5>
                  <div class="ps-2">
                      <h6 class="mb-0">${post.genderAge}</h6>
                  </div>
              </div>
              <div class="d-flex mb-3">
                  <small class="border-end me-3 pe-3"><i class="fa fa-bed text-primary me-2"></i>${post.bed} Giường</small>
                  <small class="border-end me-3 pe-3"><i class="fa fa-bath text-primary me-2"></i>${post.bath} Phòng tắm</small>
                  <small><i class="fa fa-wifi text-primary me-2"></i>Wifi</small>
              </div>
              <p class="text-body mb-3">${post.shortDes}</p>
              <div class="d-flex justify-content-between">
                <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="Details(${post.id})"> Chi Tiết </a>
                  <div>
                      <a class="btn btn-outline-light btn-social" target="_blank" rel="nofollow" href="${post.contact[2]}"><img src="img/facebook-seeklogo.com.svg" width="25px" height="25px"></img></a>
                      <a class="btn btn-outline-light btn-social" target="_blank" rel="nofollow" href="${post.contact[3]}"><img src="img/zalo-seeklogo.com.svg" width="25px" height="25px"></img></i></a>
                  </div>
              </div>
          </div>
      </div>
  </div>`;
})
           var htmls = html.join('');    
           document.getElementById("NormalNews1").innerHTML = htmls;
})
}

function getHotNews2() {
  const url1 = "../json/Roommate.json";
fetch(url1)
.then((response) => response.json()) // parse the JSON from the server
.then(function(posts) {

  var hotposts = posts.filter((post) => post.id > 6 && post.id < 10);
      var html = hotposts.map(function(post) {          
          return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.6s">
          <div class="room-item shadow rounded overflow-hidden">
              <div class="position-relative zoom">
                  <img class="img-fluid" src="${post.image1}" alt="">
                  <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
              </div>
              <div class="p-4 mt-2">
                  <div class="d-flex justify-content-between mb-3">
                      <h5 class="mb-0">${post.title}</h5>
                      <div class="ps-2">
                          <h6 class="mb-0">${post.genderAge}</h6>
                      </div>
                  </div>
                  <div class="d-flex mb-3">
                      <small class="border-end me-3 pe-3"><i class="fa fa-bed text-primary me-2"></i>${post.bed} Giường</small>
                      <small class="border-end me-3 pe-3"><i class="fa fa-bath text-primary me-2"></i>${post.bath} Phòng tắm</small>
                      <small><i class="fa fa-wifi text-primary me-2"></i>Wifi</small>
                  </div>
                  <p class="text-body mb-3">${post.shortDes}</p>
                  <div class="d-flex justify-content-between">
                    <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="Details(${post.id})"> Chi Tiết </a>
                      <div>
                          <a class="btn btn-outline-light btn-social" target="_blank" rel="nofollow" href="${post.contact[2]}"><img src="img/facebook-seeklogo.com.svg" width="25px" height="25px"></img></a>
                          <a class="btn btn-outline-light btn-social"target="_blank" rel="nofollow"  href="${post.contact[3]}"><img src="img/zalo-seeklogo.com.svg" width="25px" height="25px"></img></i></a>

                      </div>
                  </div>
              </div>
          </div>
      </div>`;
})
               var htmls = html.join('');    
               document.getElementById("HotNews2").innerHTML = htmls;
})
}

function getNormalNews2() {
  const url2 = "../json/Roommate.json";
  fetch(url2)
.then((response) => response.json()) // parse the JSON from the server
.then(function(posts) {

  var normalposts = posts.filter((post) => post.id > 9 && post.id < 13);
  var html = normalposts.map(function(post) {          
    return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.6s">
    <div class="room-item shadow rounded overflow-hidden">
        <div class="position-relative zoom">
            <img class="img-fluid" src="${post.image1}" alt="">
            <small class="position-absolute start-0 top-100 translate-middle-y bg-primary text-white rounded py-1 px-3 ms-4">${post.price}</small>
        </div>
        <div class="p-4 mt-2">
            <div class="d-flex justify-content-between mb-3">
                <h5 class="mb-0">${post.title}</h5>
                <div class="ps-2">
                    <h6 class="mb-0">${post.genderAge}</h6>
                </div>
            </div>
            <div class="d-flex mb-3">
                <small class="border-end me-3 pe-3"><i class="fa fa-bed text-primary me-2"></i>${post.bed} Giường</small>
                <small class="border-end me-3 pe-3"><i class="fa fa-bath text-primary me-2"></i>${post.bath} Phòng tắm</small>
                <small><i class="fa fa-wifi text-primary me-2"></i>Wifi</small>
            </div>
            <p class="text-body mb-3">${post.shortDes}</p>
            <div class="d-flex justify-content-between">
              <a class="btn btn-sm btn-primary rounded py-2 px-4" onclick="Details(${post.id})"> Chi Tiết </a>
                <div>
                    <a class="btn btn-outline-light btn-social" target="_blank" rel="nofollow" href="${post.contact[2]}"><img src="img/facebook-seeklogo.com.svg" width="25px" height="25px"></img></a>
                    <a class="btn btn-outline-light btn-social" target="_blank" rel="nofollow" href="${post.contact[3]}"><img src="img/zalo-seeklogo.com.svg" width="25px" height="25px"></img></i></a>
                </div>
            </div>
        </div>
    </div>
</div>`;
})
         var htmls = html.join('');    
         document.getElementById("NormalNews2").innerHTML = htmls;
})
}

function ShowPostDetails() {
    const url3 = "../json/Roommate.json";
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
        return `<div class="aa-properties-content">            
        <div class="aa-properties-details">
         <div class="aa-properties-details-img">
           
          <!-- Carousel Start -->
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
              <div class="carousel-item">
                <img class="w-100" src="${post.image4}" alt="Image">
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
  <!-- Carousel End -->
</div>
         <div class="aa-properties-info mb-0">
           <h1 style="color: #FEA116; margin-top: 15px;">${post.title}</h1>
          
            <h5><i class="fa-solid fa-venus-mars", style="color: #FEA116;"></i>&nbsp; ${post.genderAge}</h5>
        
           <h5><i class="fa-solid fa-money-bill", style="color: #FEA116;"></i> &nbsp; ${post.price}</h5>
           <h5><i class="fa-solid fa-location-dot", style="color: #FEA116;"></i> &nbsp; ${post.address}</h5>
           <p style="text-align: justify;">${post.description[0]}</p>
           <p style="text-align: justify;">${post.description[1]}</p>
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
            <h5 class = "">${post.title}</h5>
          <span>Số điện thoại: </span>
                <a  rel="nofollow" href="tel:${post.contact[1]}">
                  <i></i> ${post.contact[1]}</a>
                  <br>
            <span>Facebook: </span>
            <a  rel="nofollow" target="_blank" href="${post.contact[2]}">
              <i></i>${post.title}</a>
              <br>
         
            <span>${post.address} </span>                  
                  
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
    window.location.href = "RoommateDetails.html";
    }




