@ModelType PendingRequestInfo?
@Code
    ViewData("Title") = "Home Page"
End Code

@If Not Request.IsAuthenticated Then
    @<text>
        <form>
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" class="form-control" id="name" placeholder="John Doe">
            </div>
            <div class="form-group">
                <label for="zip">ZIP</label>
                <input type="number" class="form-control" name="zip" id="zip" onchange="showHospitalNames()">

            </div>
            <div class="form-group">
                <label for="hospital">Hospital Name</label>
                <select class="form-control" id="hospital">
                </select>
            </div>
        </form>
    </text>
End If

@section scripts
<script>
    var result = null;
    $(function () {
        navigator.geolocation.getCurrentPosition(showPosition, positionError);

        function showPosition(position) {
            var coordinates = position.coords;

            var me = { lat: coordinates.latitude, lon: coordinates.longitude };
            
            function makeAjaxFindCall(distance) {
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(me),
                    url: "../api/find/" + distance,
                    contentType: "application/json"
                }).done(function (res) {
                    result = res;
                });
            }

            @If Request.IsAuthenticated Then
                @:makeAjaxFindCall(30);
            End If
        }

        function positionError(position) {
            alert("Error! Code: " + position.code)
        }
    });

    function showHospitalNames() {
        if ($('#zip').val().length === 5) {
            $('#hospital').append(`
                    @For Each h In Hospitals
                        @:<option class="asp-removable">@h.Name</option>
                    Next
                    `);
        }
        else {
            $('.asp-removable').remove()
        }
    }
</script>
End Section

@Code

End Code