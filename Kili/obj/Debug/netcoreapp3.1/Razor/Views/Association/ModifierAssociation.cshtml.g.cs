#pragma checksum "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "28905efb6f847afe8dd08f1dc94a3f53e5d1afab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Association_ModifierAssociation), @"mvc.1.0.view", @"/Views/Association/ModifierAssociation.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
using Kili.Models.General;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28905efb6f847afe8dd08f1dc94a3f53e5d1afab", @"/Views/Association/ModifierAssociation.cshtml")]
    #nullable restore
    public class Views_Association_ModifierAssociation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Kili.ViewModels.CreerAssociationViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/jquery-3.3.1.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/jquery.validate-vsdoc.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/jquery.validate.unobtrusive.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 5 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
  
    Layout = "_Layout";
    ViewBag.Title = "ModifierAssociation";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <h1>\r\n       Modifier les données de l\'association\r\n    </h1>\r\n\r\n    <p>\r\n        Compléter les champs ci-dessous :\r\n    </p>\r\n");
#nullable restore
#line 17 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
 using (Html.BeginForm("ModifierAssociation","Association"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <fieldset>\r\n        <Label>Nom de l\'association</Label>\r\n        ");
#nullable restore
#line 21 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.TextBoxFor(m => m.association.Nom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 22 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.ValidationMessageFor(m => m.association.Nom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <br /><br />\r\n        <Label>Adresse de l\'association</Label><br />\r\n        <Label>Numéro :</Label>\r\n        ");
#nullable restore
#line 26 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.TextBoxFor(m => m.association.Adresse.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <Label>Voie :</Label>\r\n        ");
#nullable restore
#line 28 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.TextBoxFor(m => m.association.Adresse.Voie));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n        <Label>Compléments :</Label>\r\n        ");
#nullable restore
#line 30 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.TextBoxFor(m => m.association.Adresse.Complement));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n        <Label>Code Postal :</Label>\r\n        ");
#nullable restore
#line 32 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.TextBoxFor(m => m.association.Adresse.CodePostal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <Label>Ville :</Label>\r\n        ");
#nullable restore
#line 34 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.TextBoxFor(m => m.association.Adresse.Ville));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br /><br />\r\n        <Label>Thème :</Label>\r\n        ");
#nullable restore
#line 36 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.DropDownListFor(m => m.association.Theme, new SelectList(Enum.GetValues(typeof(ThemeAssociation))), "Choisir le thème"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br /><br />\r\n        ");
#nullable restore
#line 37 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.HiddenFor(m => m.association.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n        ");
#nullable restore
#line 38 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"
   Write(Html.HiddenFor(m => m.association.AdresseId));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n        <br />\r\n        <input type=\"submit\" value=\"Enregistrer\" />\r\n    </fieldset>\r\n");
#nullable restore
#line 42 "C:\Users\User\Source\Repos\Kili\Kili\Views\Association\ModifierAssociation.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "28905efb6f847afe8dd08f1dc94a3f53e5d1afab8501", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "28905efb6f847afe8dd08f1dc94a3f53e5d1afab9627", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "28905efb6f847afe8dd08f1dc94a3f53e5d1afab10753", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Kili.ViewModels.CreerAssociationViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
