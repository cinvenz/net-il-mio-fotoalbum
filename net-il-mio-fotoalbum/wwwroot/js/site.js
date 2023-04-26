const loadPhotos = filter => getPhotos(filter).then(renderPhotos);
const getPhotos = title => axios
    .get('/api/photo', title ? { params: { title } } : {})
    .then(res => res.data);

const renderPhotos = photos => {
    const noPhotos = document.querySelector("#no-photos");
    const loader = document.querySelector("#photos-loader");
    const photosTbody = document.querySelector("#photos");
    //const photosTable = document.querySelector("#photos-table");
    const photosFilter = document.querySelector("#photos-filter");

    if (photos && photos.length > 0) {
        //photosTable.classList.add("show");
        photosFilter.classList.add("d-block");
        noPhotos.classList.add("d-none");
    }
    else { noPhotos.classList.remove("d-none"); }

    loader.classList.add("d-none");

    photosTbody.innerHTML = photos.map(photoComponent).join('');
};

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


const initFilter = () => {
    const filter = document.querySelector("#photos-filter input");
    filter.addEventListener("input", (e) => loadPhotos(e.target.value))
};

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

// <CreatePhoto>

const photoPhoto = photo => axios
    .post("/Api/Photo", photo)
    .then(() => location.href = "/Photo/ApiIndex")
    .catch(err => renderErrors(err.response.data.errors));

const initCreateForm = () => {
    const form = document.querySelector("#photo-create-form");

    form.addEventListener("submit", e => {
        e.preventDefault();

        const photo = getPhotoFromForm(form);
        photoPhoto(photo);
    });
};

const getPhotoFromForm = form => {
    const title = form.querySelector("#title").value;
    const description = form.querySelector("#description").value;
    const image = form.querySelector("#image").value;
   

    return {
        title,
        description,
        image,
    };
};


const renderErrors = errors => {
    const titleErrors = document.querySelector("#title-errors");
    const descriptionErrors = document.querySelector("#description-errors");

    titleErrors.innerText = errors.Name?.join("\n") ?? "";
    descriptionErrors.innerText = errors.Description?.join("\n") ?? "";
};


const getPhoto = id => axios
    .get(`/api/photo/${id}`)
    .then(res => res.data);



