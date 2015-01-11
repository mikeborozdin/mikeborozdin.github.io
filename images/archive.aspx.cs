#region Using

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BlogEngine.Core;

#endregion

public partial class archive : BlogEngine.Core.Web.Controls.BlogBasePage
{
	/// <summary>
	/// Handles the Load event of the Page control.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack && !IsCallback)
    {
      CreateArchive();
      AddTotals();
    }

    Page.Title += " - " +Resources.labels.archive;
    base.AddMetaTag("description", Resources.labels.archive + " | " + BlogSettings.Instance.Name);
  }

	/// <summary>
	/// Creates the category top menu.
	/// </summary>
  private void CreateMenu()
  {
    foreach (Category cat in Category.Categories)
    {
      HtmlAnchor a = new HtmlAnchor();
      a.InnerHtml = cat.Title;
      a.HRef = "#" + Utils.RemoveIllegalCharacters(cat.Title);
      a.Attributes.Add("rel", "directory");

      HtmlGenericControl li = new HtmlGenericControl("li");
      li.Controls.Add(a);
      ulMenu.Controls.Add(li);
    }
  }

	/// <summary>
	/// Sorts the categories.
	/// </summary>
	/// <param name="categories">The categories.</param>
  private SortedDictionary<string, Guid> SortCategories(Dictionary<Guid, string> categories)
  {
    SortedDictionary<string, Guid> dic = new SortedDictionary<string, Guid>();
    foreach (Category cat in Category.Categories)
    {
      dic.Add(cat.Title, cat.Id);
    }

    return dic;
  }

  private void CreateArchive()
  {
      List<Post> list = Post.Posts;

      HtmlTable table = CreateTable("");

      foreach (Post post in list)
      {
				if (!post.IsVisible)
					continue;

        HtmlTableRow row = new HtmlTableRow();

        HtmlTableCell date = new HtmlTableCell();
        date.InnerHtml = post.DateCreated.ToString("yyyy-MM-dd");
        date.Attributes.Add("class", "date");
        row.Cells.Add(date);

        HtmlTableCell title = new HtmlTableCell();
        title.InnerHtml = string.Format("<a href=\"{0}\">{1}</a>", post.RelativeLink, post.Title);
        title.Attributes.Add("class", "title");
        row.Cells.Add(title);

        if (BlogSettings.Instance.IsCommentsEnabled)
        {
          HtmlTableCell comments = new HtmlTableCell();
          comments.InnerHtml = post.ApprovedComments.Count.ToString();
          comments.Attributes.Add("class", "comments");
          row.Cells.Add(comments);
        }

        if (BlogSettings.Instance.EnableRating)
        {
          HtmlTableCell rating = new HtmlTableCell();
          rating.InnerHtml = post.Raters == 0 ? "None" : Math.Round(post.Rating, 1).ToString();
          rating.Attributes.Add("class", "rating");
          row.Cells.Add(rating);
        }

        table.Rows.Add(row);
      }

      phArchive.Controls.Add(table);
  }

  private HtmlTable CreateTable(string name)
  {
    HtmlTable table = new HtmlTable();
    table.Attributes.Add("summary", name);

    HtmlTableRow header = new HtmlTableRow();

    HtmlTableCell date = new HtmlTableCell("th");
    date.InnerHtml = base.Translate("date");
    header.Cells.Add(date);

    HtmlTableCell title = new HtmlTableCell("th");
    title.InnerHtml = base.Translate("title");
    header.Cells.Add(title);

    if (BlogSettings.Instance.IsCommentsEnabled)
    {
      HtmlTableCell comments = new HtmlTableCell("th");
      comments.InnerHtml = base.Translate("comments");
      comments.Attributes.Add("class", "comments");
      header.Cells.Add(comments);
    }

    if (BlogSettings.Instance.EnableRating)
    {
      HtmlTableCell rating = new HtmlTableCell("th");
      rating.InnerHtml = base.Translate("rating");
      rating.Attributes.Add("class", "rating");
      header.Cells.Add(rating);
    }

    table.Rows.Add(header);

    return table;
  }

  private void AddTotals()
  {
    int comments = 0;
    int raters = 0;
    foreach (Post post in Post.Posts)
    {
      comments += post.ApprovedComments.Count;
      raters += post.Raters;
    }

    ltPosts.Text = Post.Posts.Count + " " + Resources.labels.posts.ToLowerInvariant();
    if (BlogSettings.Instance.IsCommentsEnabled)
      ltComments.Text = comments + " " + Resources.labels.comments.ToLowerInvariant();

    if (BlogSettings.Instance.EnableRating)
      ltRaters.Text = raters + " " + Resources.labels.raters.ToLowerInvariant();
  }
}
