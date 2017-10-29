using System;
using System.Collections.Generic;
using AutoFixture.Kernel;

namespace AutoFixture
{
    /// <summary>
    /// A marker class, used to explicitly identify the predefined builders role in an 
    /// <see cref="ISpecimenBuilderNode" /> graph.
    /// </summary>
    /// <remarks>
    /// The only purpose of this class is to act as an easily identifiable
    /// container. This makes it easier to find the collection of
    /// predefined builders even if it is buried deep in a larger graph.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", 
        Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class PredefinedBuildersNode : ISpecimenBuilderNode
    {

        /// <summary>
        /// Creates a new instance of <see cref="PredefinedBuildersNode"/>.
        /// </summary>
        /// <param name="builder">The target specimen builder node.</param>
        public PredefinedBuildersNode(ISpecimenBuilder builder)
        {
            this.Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <inheritdoc />
        public ISpecimenBuilderNode Compose(IEnumerable<ISpecimenBuilder> builders)
        {
            var composedBuilder =
                CompositeSpecimenBuilder.ComposeIfMultiple(builders);
            return new PredefinedBuildersNode(composedBuilder);
        }

        /// <inheritdoc />
        public object Create(object request, ISpecimenContext context)
        {
            return this.Builder.Create(request, context);
        }

        /// <inheritdoc />
        public IEnumerator<ISpecimenBuilder> GetEnumerator()
        {
            yield return this.Builder;
        }

        /// <inheritdoc />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>Gets the builder decorated by this instance.</summary>
        /// <value>The builder originally supplied via the constructor.</value>
        public ISpecimenBuilder Builder { get; }
    }
}
