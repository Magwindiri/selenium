#pragma checksum "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4477aac23e22ee4fdd5d84635257e48aed15cf63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Apply_ApplicationComplete), @"mvc.1.0.view", @"/Views/Apply/ApplicationComplete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Apply/ApplicationComplete.cshtml", typeof(AspNetCore.Views_Apply_ApplicationComplete))]
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
#line 1 "C:\_devprojects\selenium\CreditCards\Views\_ViewImports.cshtml"
using CreditCards;

#line default
#line hidden
#line 2 "C:\_devprojects\selenium\CreditCards\Views\_ViewImports.cshtml"
using CreditCards.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4477aac23e22ee4fdd5d84635257e48aed15cf63", @"/Views/Apply/ApplicationComplete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76cefc3674abbf02ed0c8ae3fc240884dc47ab85", @"/Views/_ViewImports.cshtml")]
    public class Views_Apply_ApplicationComplete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CreditCards.Core.Model.CreditCardApplication>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(52, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
  
    ViewData["Title"] = "Application Complete";

#line default
#line hidden
            BeginContext(106, 454, true);
            WriteLiteral(@"
<h2>Application Complete</h2>
<div class=""row"">
    <div class=""col-md-3"">
        <div class=""panel panel-primary"">
            <div class=""panel-heading"">
                Application Details
            </div>
            <div class=""panel panel-default"">
                <div class=""panel-heading"">
                    <p>Application Decision</p>
                </div>
                <div class=""panel-body"" id=""Decision"">
                    ");
            EndContext();
            BeginContext(561, 14, false);
#line 19 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.Decision);

#line default
#line hidden
            EndContext();
            BeginContext(575, 235, true);
            WriteLiteral("\r\n                </div>\r\n                <div class=\"panel-heading\">\r\n                    <p>Application Reference Number</p>\r\n                </div>\r\n                <div class=\"panel-body\" id=\"ReferenceNumber\">\r\n                    ");
            EndContext();
            BeginContext(811, 8, false);
#line 25 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.Id);

#line default
#line hidden
            EndContext();
            BeginContext(819, 206, true);
            WriteLiteral("\r\n                </div>\r\n\r\n                <div class=\"panel-heading\">\r\n                    <p>Name</p>\r\n                </div>\r\n                <div class=\"panel-body\" id=\"FullName\">\r\n                    ");
            EndContext();
            BeginContext(1026, 15, false);
#line 32 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(1041, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1043, 14, false);
#line 32 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
                                Write(Model.LastName);

#line default
#line hidden
            EndContext();
            BeginContext(1057, 200, true);
            WriteLiteral("\r\n                </div>\r\n\r\n                <div class=\"panel-heading\">\r\n                    <p>Age</p>\r\n                </div>\r\n                <div class=\"panel-body\" id=\"Age\">\r\n                    ");
            EndContext();
            BeginContext(1258, 9, false);
#line 39 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.Age);

#line default
#line hidden
            EndContext();
            BeginContext(1267, 219, true);
            WriteLiteral("\r\n                </div>\r\n\r\n                <div class=\"panel-heading\">\r\n                    <p>Gross Annual Income</p>\r\n                </div>\r\n                <div class=\"panel-body\" id=\"Income\">\r\n                    ");
            EndContext();
            BeginContext(1487, 23, false);
#line 46 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.GrossAnnualIncome);

#line default
#line hidden
            EndContext();
            BeginContext(1510, 231, true);
            WriteLiteral("\r\n                </div>\r\n\r\n                <div class=\"panel-heading\">\r\n                    <p>Relationship Status</p>\r\n                </div>\r\n                <div class=\"panel-body\" id=\"RelationshipStatus\">\r\n                    ");
            EndContext();
            BeginContext(1742, 24, false);
#line 53 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.RelationshipStatus);

#line default
#line hidden
            EndContext();
            BeginContext(1766, 226, true);
            WriteLiteral("\r\n                </div>\r\n\r\n                <div class=\"panel-heading\">\r\n                    <p>Source Of Business</p>\r\n                </div>\r\n                <div class=\"panel-body\" id=\"BusinessSource\">\r\n                    ");
            EndContext();
            BeginContext(1993, 20, false);
#line 60 "C:\_devprojects\selenium\CreditCards\Views\Apply\ApplicationComplete.cshtml"
               Write(Model.BusinessSource);

#line default
#line hidden
            EndContext();
            BeginContext(2013, 77, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\n        </div>\n    </div>\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CreditCards.Core.Model.CreditCardApplication> Html { get; private set; }
    }
}
#pragma warning restore 1591
