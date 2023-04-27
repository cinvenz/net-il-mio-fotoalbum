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



const photoComponent = photo => `
<div class="">
    <div class="card" style="width: 18rem; height: 25rem;">
        <img src="${photo.image}" class="card-img-top" alt="photo">
        <div class="card-body">
            <h5 class="card-title  ms-lg-3"><a href="/photo/detail/${photo.id}">${photo.title}</a></h5>
            <p class="card-text  ms-lg-3">${photo.description}</p>
            <div class="text-center">
            </div>
        </div>
    </div>
</div>`;



const cardDetails = photo => `<h2 class="text-center mb-3 text-white my-5">${photo.title}</h2>
    <img class="container-fluid" src="${photo.image}" />

    <div class="px-5 mt-3 d-flex justify-content-between gap-3">
        <div class="container">
            <span class="text-white-50">Description</span>
            <p class="text-white">${photo.description}</p>
        </div>
        <div class="container">
                <span class="text-white-50">Tags</span>
            <p id="tags" class="text-white"></p>

        </div>

    </div>`;


function initDetails() {
    var id = Number(location.pathname.split("/")[3])
    getPhoto(id).then(photo => renderPhoto(photo));
    initComments(id);
}

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




