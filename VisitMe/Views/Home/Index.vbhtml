@ModelType PendingRequestInfo?
@Code
    ViewData("Title") = "Home Page"
End Code

@If Model.HasValue Then
    If Request.IsAuthenticated Then
        Using Html.BeginForm("AnswerRequestAsync", "Home", FormMethod.Post)
            @<text>
                <div class="form-group">
                    <p>You have been assigned a request at @Model.Value.Hospital.Name.</p>
                </div>
                <div class="form-group">
                    <label for="name">Name</label>
                    <input type="text" class="form-control" name="name" id="name" required="required" />
                </div>
                <div class="form-group">
                    <label for="church">Church name</label>
                    <input type="text" class="form-control" name="church" id="church" required="required" />
                </div>
                <div class="form-group">
                    <label for="title">Position</label>
                    <input type="text" class="form-control" name="title" id="title" required="required" />
                </div>
                <div class="form-group">
                    <label for="phone">Phone number</label>
                    <input type="text" class="form-control" name="phone" id="phone" required="required" />
                </div>
                <div class="form-group row">
                    <div class="col">
                        <button name="action" value="accept">Accept</button>
                    </div>
                    <div class="col">
                        <button name="action" value="reject">Reject</button>
                    </div>
                </div>
                <div hidden="hidden">
                    <input name="ID" type="number" value="@Model.Value.ID" />
                </div>
            </text>
        End Using
    Else
        @<text>
            <div>
                Your request (ID: @Model.Value.ID) has been received. Save the ID number to track your request.
            </div>
        </text>
    End If
Else
    If Request.IsAuthenticated Then
        @:<div id="placeholder" />
    Else
        Using Html.BeginForm("CreateVisitAsync", "Home", FormMethod.Post)
            @<text>
                <div class="form-group">
                    <label for="name">Name</label>
                    <input type="text" class="form-control" name="name" id="name" placeholder="John Doe">
                    @Html.ValidationMessage("exists")
                </div>
                <div class="form-group">
                    <label for="zip">ZIP</label>
                    <input type="number" Class="form-control" name="zip" id="zip" onchange="showHospitalNames()">
                    @Html.ValidationMessage("ZIP")
                </div>
                <div class="form-group">
                    <label for="hospital">Hospital Name</label>
                    <select class="form-control" name="hospital" id="hospital"></select>
                </div>
                <div class="form-group">
                    <label for="room">Room number</label>
                    <input type="text" class="form-control" name="room" id="room" />
                </div>
                <div class="form-group">
                    <label for="info">Additional navigation information?</label>
                    <input type="text" class="form-control" name="info" id="info" />
                </div>
                <button type="submit" class="btn btn-primary">Submit</button>
            </text>
        End Using
    End If
End If

@section scripts
    <script>
        var result = null;
        function find(distance, funccall) {
            navigator.geolocation.getCurrentPosition(showPosition, positionError);

            function showPosition(position) {
                var coordinates = position.coords;

                var me = { lat: coordinates.latitude, lon: coordinates.longitude };

                $.ajax({
                    type: "POST",
                    async: false,
                    data: JSON.stringify(me),
                    url: "../api/find/" + distance,
                    contentType: "application/json",
                    success: function (res) {
                        funccall(res);
                    }
                });
            }

            function positionError(position) {
                alert("Error! Code: " + position.code)
            }
        }

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

        function post(path, params, method) {
            method = method || "post";

            var form = document.createElement("form");
            form.setAttribute("method", method);
            form.setAttribute("action", path);
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", "data");
            hiddenField.setAttribute("value", JSON.stringify(params));

            form.appendChild(hiddenField)
            document.body.appendChild(form);
            form.submit();
        }
        @If Request.IsAuthenticated And Model Is Nothing Then
        @<text>
        $(function () {
            find(30, function (res) {
                console.log(JSON.stringify(res))
                if (JSON.stringify(res) == `{"id":0,"hospital":{"name":null,"loc":{"lat":0,"lon":0},"addr":null,"city":null,"state":0,"zip":0}}`) {
                    $("#placeholder").append(`<input type="text" readonly class="form-control-plaintext">`);
                } else {
                    post("/Home/VisitFound", res);
                }
            });
        });
        </text>
        End If
    </script>
End Section