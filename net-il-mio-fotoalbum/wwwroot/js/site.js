const loadPhotos = filter => getPhotos(filter).then(renderPhotos);

const getPhotos = name => axios
    .get('/api/photo', name ? { params: { name } } : {})
    .then(res => res.data);

const renderPhotos = photos => {
    const cards = document.getElementById('photos-filter');
    cards.innerHTML = photos.map(photoComponent).join('');
}

const photoComponent = photo => `
<div class="">
    <div class="card" style="width: 18rem; height: 25rem;">
        <img src="@photo.Image" class="card-img-top" alt="Photo">
        <div class="card-body">
            <h5 class="card-title  ms-lg-3"><a href="@Url.Action("Detail", "Photo", new { Id = photo.Id })">@photo.Title</a></h5>
            <p class="card-text  ms-lg-3">@photo.Description</p>
            @*<h6 class="ms-lg-3">@photo.Category!.Title</h6>*@
            <div class="text-center">
                <a href="@Url.Action("Update", "Photo", new { Id = photo.Id})">Edit</a>
                <form asp-action="Delete" asp-controller="Photo" asp-route-id="@photo.Id">
                    @Html.AntiForgeryToken()
                    <button type="submit">Delete</button>
                </form>
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
    .photo("/api/photo", photo)
    .then(() => location.href = "/photo/apiindex")
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
    //const imageFile = form.querySelector("#image-file");
    //const categories = form.querySelectorAll("#categories input");

    return {
        id: 0,
        title,
        description,
        image,
        //imageFile,
        //categories
    };
};

const renderErrors = errors => {
    const titleErrors = document.querySelector("#title-errors");
    const descriptionErrors = document.querySelector("#description-errors");

    titleErrors.innerText = errors.Name?.join("\n") ?? "";
    descriptionErrors.innerText = errors.Description?.join("\n") ?? "";
    categoryIdErrors.innerText = errors.CategoryId?.join("\n") ?? "";
};


const getPhoto = id => axios
    .get(`/api/photo/${id}`)
    .then(res => res.data);



function deletePhoto(id) {
    axios.delete(`/Api/Photo/${id}`)
        .then(function (response) {
            console.log(response)
        }).catch(function (error) {
            console.log(error)
        });
    location.reload()
}