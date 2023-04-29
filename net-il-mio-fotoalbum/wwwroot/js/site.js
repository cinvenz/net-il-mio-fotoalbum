const loadPhotos = filter => getPhotos(filter).then(renderPhotos);
const getPhotos = title => axios
    .get('/api/photo', title ? { params: { title } } : {})
    .then(res => ({ photos: res.data, visible: true }));

const renderPhotos = ({ photos }) => {
    const noPhotos = document.querySelector("#no-photos");
    const loader = document.querySelector("#photos-loader");
    const photosTbody = document.querySelector("#photos");
    //const photosTable = document.querySelector("#photos-table");
    const photosFilter = document.querySelector("#photos-filter");

    //if (!visible) {
    //    photosTbody.classList.add("d-none");
    //}

    if (photos && photos.length > 0) {
        //photosTable.classList.add("show");
        photosFilter.classList.add("d-block");
        noPhotos.classList.add("d-none");
    }
    else { noPhotos.classList.remove("d-none"); }

    loader.classList.add("d-none");

    photosTbody.innerHTML = photos.map(photoComponent).join('');
};
const initFilter = () => {
    const filter = document.querySelector("#photos-filter input");
    filter.addEventListener("input", (e) => loadPhotos(e.target.value))
};

const getPhoto = id => axios
    .get(`/api/photo/${id}`)
    .then(res => res.data);



function initDetail() {
    var id = Number(location.pathname.split("/")[3])
    getPhoto(id).then(photo => renderPhoto(photo));
}

const renderPhoto = photo => {
    const pagina = document.getElementById('detail');
    pagina.innerHTML = photoDetail(photo);
}

const photoComponent = photo => `
<div class="card text-bg-dark ">
  <img src="${photo.image}" class="card-img" alt="${photo.title}">
  <div class="card-img-overlay">
    <h5 class="card-title">${photo.title}</h5>
    <p class="card-text">${photo.description}</p>
    <a class="btn btn-outline-light" href="/Photo/ApiDetail/${photo.id}">Dettagli</a>
  </div>
</div>`;

const photoDetail = photo => `
    <h2>${photo.title}</h2>

    <div class="container">
	    <div class="pb-4">
		    <img src="${photo.image}" class=" detail-img " alt="photo">
	    </div>
	    <div>
		   <p class="card-text  ms-lg-3">${photo.description}</p>
	    </div>
    </div>
`


// <Categories>

const loadCategories = () => getCategories().then(renderCategories);

const getCategories = () => axios
    .get("/api/category")
    .then(res => res.data);

const renderCategories = categories=> {
    const CategoriesSelection = document.querySelector("#categories");
    CategoriesSelection.innerHTML = Categories.map(categoryOptionComponent).join('');
}

const categoryOptionComponent = category => `
	<div class="flex gap">
		<input id="${category.id}" type="checkbox" />
		<label for="${category.id}">${category.title}</label>
	</div>`;

// </Categories>

// <CreateMessage>

const CreateMessage = message => axios
    .post('/Api/Photo', message)
    .then(res => console.log(res.data))
    .catch(e => renderErrors(e.response.data.errors));

const initMessageForm = () => {
    const form = document.querySelector('#message-form');
    const email = document.querySelector('#email');
    const messageText = document.querySelector('#message');

    form.addEventListener("submit", e => {
        e.preventDefault();

        const message = getMessageFromForm(form);
        CreateMessage(message);
        email.value = '';
        messageText.value = '';
    });
}

const getMessageFromForm = form => {
    const email = form.querySelector('#email').value;
    const messageText = form.querySelector('#message').value;
    return {
        email,
        messageText
    }
}



const renderErrors = errors => {
    const titleErrors = document.querySelector("#title-errors");
    const descriptionErrors = document.querySelector("#description-errors");

    titleErrors.innerText = errors.Name?.join("\n") ?? "";
    descriptionErrors.innerText = errors.Description?.join("\n") ?? "";
};


const images = [
    "https://www.turismoroma.it/sites/default/files/colosseo_slide_0.jpg",
    "https://media-assets.ad-italia.it/photos/62961fb73236e8dd2aa42f9c/16:9/w_2992,h_1683,c_limit/GettyImages-1174275340.jpg",
    "https://media-assets.vanityfair.it/photos/614c9482e6e64c0e0b190706/16:9/pass/Statua-della-Libert%C3%A0-Portrait.jpeg",
    "https://media.vaticannews.va/media/content/dam-archive/vaticannews/multimedia/2021/10/11/AdobeStock_109601187AEM.jpg/_jcr_content/renditions/cq5dam.thumbnail.cropped.750.422.jpeg",
];

const carouselImages = document.querySelectorAll(".carousel-item img");

carouselImages.forEach((img, index) => {
    img.src = images[index];
    img.alt = `Image ${index + 1}`;
});

