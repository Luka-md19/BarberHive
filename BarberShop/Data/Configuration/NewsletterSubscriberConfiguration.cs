using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.HasKey(ns => ns.Email);

        builder.Property(ns => ns.IsSubscribed)
            .IsRequired();

        builder.ToTable("NewsletterSubscribers");
    }
}
