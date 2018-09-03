@ModelType Patient
@Code
    ViewData("Title") = "Visit"
End Code

<div>
    <h2>Thanks for your help!</h2>
    <p>Patient details below.</p>
</div>

<div class="row">
    Name: @Model.Name
</div>
<div class="row">
    Room: @Model.Room
</div>
<div class="row">
    Phone: @Model.PhoneNumber
</div>
<div class="row">
    Location: @Model.Hospital.Name
</div>
<div class="row">
    Address: @Model.Hospital.Address
</div>